using System.Buffers.Binary;
using Katalogos.Diktyo.Structure;

namespace Katalogos.Diktyo.Framing.Writing.Metadata;

public class FrameMetadataWriter : IFrameMetadataWriter
{
    public bool TryWrite(Span<byte> buffer, FrameMetadata metadata, PacketOrigin origin)
    {
        return
            BinaryPrimitives.TryWriteInt16BigEndian(buffer, (short)metadata.MessageId) &&
            BinaryPrimitives.TryWriteInt32BigEndian(buffer[sizeof(short)..], metadata.PayloadLength) &&
            (origin != PacketOrigin.Client ||
             BinaryPrimitives.TryWriteInt32BigEndian(buffer[sizeof(int)..], metadata.InstanceId));
    }
}