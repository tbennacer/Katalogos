using System.Buffers;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using Katalogos.Diktyo.Framing.Reader.Metadata;
using Katalogos.Diktyo.Structure;

namespace Katalogos.Diktyo.Framing.Reader;

public class FrameReader : IFrameReader
{
    private readonly IFrameMetadataReader _frameMetadataReader;

    public FrameReader(IFrameMetadataReader frameMetadataReader)
    {
        _frameMetadataReader = frameMetadataReader;
    }

    public async ValueTask<Frame> ReadFrameAsync(PipeReader reader, CancellationToken cancellationToken)
    {
        Frame? frame = default;
        while (!cancellationToken.IsCancellationRequested)
        {
            ReadResult readResult = await reader.ReadAsync(cancellationToken).ConfigureAwait(false);
            ReadOnlySequence<byte> buffer = readResult.Buffer;

            SequencePosition consumed = buffer.Start;

            bool frameFound = TryFindFrame(ref buffer, out FrameMetadata? metadata, out SequencePosition frameEnd);

            if (frameFound)
            {
                frame = new Frame(metadata!, buffer.Slice(consumed, frameEnd));
                reader.AdvanceTo(consumed, frameEnd);
                break;
            }

            if (!readResult.IsCompleted) continue;

            reader.AdvanceTo(buffer.Start, buffer.End);

            throw new InvalidDataException("Incomplete frame received before the end of the connection.");
        }

        return frame!;
    }

    public async IAsyncEnumerable<Frame> ReadFramesAsync(PipeReader reader,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
            yield return await ReadFrameAsync(reader, cancellationToken).ConfigureAwait(false);
    }

    private bool TryFindFrame(ref ReadOnlySequence<byte> buffer,
        out FrameMetadata? metadata,
        out SequencePosition frameEnd)
    {
        metadata = default;
        frameEnd = default;

        SequenceReader<byte> bufferReader = new(buffer);
        if (!_frameMetadataReader.TryRead(ref bufferReader, PacketSender.Server, out metadata))
            return false;

        int frameLength = metadata!.PayloadLength;
        if (frameLength > buffer.Length)
            return false;

        bufferReader.Advance(frameLength);
        frameEnd = bufferReader.Position;

        return true;
    }
}