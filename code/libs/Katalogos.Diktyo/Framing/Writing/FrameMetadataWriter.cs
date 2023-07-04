using System.Buffers.Binary;

namespace Katalogos.Diktyo.Framing.Writing;

public class FrameMetadataWriter
{
    public bool TryWrite(ref Span<byte> buffer, FrameMetadata metadata)
    {
        return
            BinaryPrimitives.TryWriteInt16BigEndian(buffer, (short)metadata.MessageId) &&
            BinaryPrimitives.TryWriteInt32BigEndian(buffer[sizeof(short)..], metadata.PayloadLength) &&
            BinaryPrimitives.TryWriteInt32BigEndian(buffer[sizeof(int)..], metadata.InstanceId);
    }
}