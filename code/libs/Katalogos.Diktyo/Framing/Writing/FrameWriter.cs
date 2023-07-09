using System.Buffers;
using System.IO.Pipelines;
using Katalogos.Diktyo.Framing.Writing.Metadata;
using Katalogos.Diktyo.Structure;

namespace Katalogos.Diktyo.Framing.Writing;

public class FrameWriter : IFrameWriter
{
    private readonly IFrameMetadataWriter _frameMetadataWriter;
    private readonly IFramePacker _framePacker;

    public FrameWriter(IFrameMetadataWriter frameMetadataWriter, IFramePacker framePacker)
    {
        _frameMetadataWriter = frameMetadataWriter;
        _framePacker = framePacker;
    }

    public ValueTask<FlushResult> WriteAsync<TMessage>(PipeWriter writer,
        TMessage message,
        PacketSender sender,
        CancellationToken token = default) where TMessage : INetworkMessage
    {
        (FrameMetadata metadata, ReadOnlySequence<byte> payload) = _framePacker.Pack(message, sender);

        var frameSize = (int)
            (sizeof(short) +
             (sender == PacketSender.Client ? sizeof(int) : 0) +
             metadata.LengthBytesCount + payload.Length);

        Span<byte> buffer = writer.GetSpan(frameSize);

        if (!_frameMetadataWriter.TryWrite(buffer, metadata, sender))
            throw new InvalidOperationException(
                $"{nameof(FrameWriter)} failed to write frame metadata into the buffer for the message: {nameof(TMessage)}.");

        if (!payload.IsSingleSegment)
            throw new InvalidDataException(
                $"Message frame {nameof(TMessage)} is invalid. Payload isn't a single segment, frame packing isn't good.");

        payload.CopyTo(buffer[(int)(frameSize - payload.Length)..]);

        writer.Advance(frameSize);

        return writer.FlushAsync(token);
    }
}