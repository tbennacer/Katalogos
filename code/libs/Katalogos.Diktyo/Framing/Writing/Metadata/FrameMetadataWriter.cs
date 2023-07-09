using System.Buffers.Binary;
using Katalogos.Diktyo.Structure;

namespace Katalogos.Diktyo.Framing.Writing.Metadata;

public class FrameMetadataWriter : IFrameMetadataWriter
{
    public bool TryWrite(Span<byte> buffer, FrameMetadata metadata, PacketSender sender)
    {
        if (!BinaryPrimitives.TryWriteInt16BigEndian(buffer,
                (short)((metadata.MessageId << 2) | metadata.LengthBytesCount))) return false;

        if (sender == PacketSender.Client &&
            !BinaryPrimitives.TryWriteInt32BigEndian(buffer[sizeof(short)..], metadata.InstanceId)) return false;

        switch (metadata.LengthBytesCount)
        {
            case 0: break;
            case 1:
                buffer[sizeof(short) + sizeof(int)] = (byte)metadata.PayloadLength;
                break;
            case 2:
                if (!BinaryPrimitives.TryWriteInt16BigEndian(buffer[(sizeof(short) + sizeof(int))..],
                        (short)metadata.PayloadLength)) return false;
                break;
            case 3:
                buffer[sizeof(short) + sizeof(int)] = (byte)((metadata.PayloadLength >> 16) & byte.MaxValue);
                if (!BinaryPrimitives.TryWriteInt16BigEndian(buffer[(sizeof(short) + sizeof(int) + sizeof(byte))..],
                        (short)(metadata.PayloadLength & ushort.MaxValue))) return false;
                break;
        }

        return true;
    }
}