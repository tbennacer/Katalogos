using System.Buffers;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using Katalogos.Diktyo.Framing.Reader.Metadata;
using Katalogos.Diktyo.Structure;

namespace Katalogos.Diktyo.Framing.Reader;

file record FrameParsingResult(FrameMetadata metadata,
    SequencePosition PayloadStartPosition,
    SequencePosition FrameEnd);

public class FrameReader : IFrameReader
{
    private readonly IFrameMetadataReader _frameMetadataReader;

    public FrameReader(IFrameMetadataReader frameMetadataReader)
    {
        _frameMetadataReader = frameMetadataReader;
    }

    public async ValueTask<Frame> ReadFrameAsync(PipeReader reader,
        PacketSender sender,
        CancellationToken cancellationToken)
    {
        Frame? frame = default;
        while (!cancellationToken.IsCancellationRequested)
        {
            ReadResult readResult = await reader.ReadAsync(cancellationToken).ConfigureAwait(false);
            ReadOnlySequence<byte> buffer = readResult.Buffer;

            SequencePosition consumed = buffer.Start;

            bool found =
                TryFindFrame(ref buffer, _frameMetadataReader, sender, out FrameParsingResult? parsingResult);

            if (found)
            {
                FrameMetadata metadata = parsingResult!.metadata;
                ReadOnlySequence<byte> payload =
                    buffer.Slice(parsingResult.PayloadStartPosition, parsingResult.FrameEnd);

                frame = new Frame(metadata, payload);
                reader.AdvanceTo(consumed, parsingResult.FrameEnd);
                break;
            }

            if (!readResult.IsCompleted) continue;

            reader.AdvanceTo(buffer.Start, buffer.End);

            throw new InvalidDataException("Incomplete frame received before the end of the connection.");
        }

        if (frame is null)
            throw new OperationCanceledException("Cancellation requested during frame reading.");

        return frame;

        static bool TryFindFrame(ref ReadOnlySequence<byte> buffer,
            IFrameMetadataReader frameMetadataReader,
            PacketSender origin, out FrameParsingResult? parsingResult)
        {
            parsingResult = default;

            SequenceReader<byte> bufferReader = new(buffer);
            if (!frameMetadataReader.TryRead(ref bufferReader, origin, out FrameMetadata? metadata))
                return false;

            int frameLength = metadata!.PayloadLength;
            if (frameLength > buffer.Length)
                return false;

            SequencePosition payloadStart = bufferReader.Position;

            bufferReader.Advance(frameLength);
            parsingResult = new FrameParsingResult(metadata!, payloadStart, bufferReader.Position);

            return true;
        }
    }

    public async IAsyncEnumerable<Frame> ReadFramesAsync(PipeReader reader,
        PacketSender sender,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
            yield return await ReadFrameAsync(reader, sender, cancellationToken).ConfigureAwait(false);
    }
}