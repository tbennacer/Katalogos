using System.Buffers;
using Katalogos.Diktyo.Structure;

namespace Katalogos.Diktyo.Framing.Reader.Metadata;

public class FrameMetadataReader : IFrameMetadataReader
{
    public bool TryRead(ref SequenceReader<byte> bufferReader, PacketSender sender, out FrameMetadata? metadata)
    {
        metadata = null;

        if (!bufferReader.TryReadBigEndian(out short header)) return false;

        int instanceId = default;
        if (sender == PacketSender.Client && !bufferReader.TryReadBigEndian(out instanceId))
            return false;

        int messageId = header >> 2;
        int lenType = header & 3;

        int payloadLength;
        switch (lenType)
        {
            case 0:
                payloadLength = 0;
                break;
            case 1:
                if (!bufferReader.TryRead(out byte byteValue)) return false;
                payloadLength = byteValue;
                break;
            case 2:
                if (!bufferReader.TryReadBigEndian(out short ushortValue)) return false;
                payloadLength = ushortValue;
                break;
            case 3:
                if (!bufferReader.TryRead(out byte sbyteValue1) ||
                    !bufferReader.TryRead(out byte sbyteValue2) ||
                    !bufferReader.TryRead(out byte sbyteValue3)) return false;

                payloadLength = ((sbyteValue1 & byte.MaxValue) << 16) +
                                ((sbyteValue2 & byte.MaxValue) << 8) +
                                (sbyteValue3 & byte.MaxValue);
                break;
            default:
                throw new InvalidDataException(
                    $"Invalid length type ({lenType}) encountered while reading {nameof(FrameMetadata)}. The length type must be inside the range [0;3].");
        }

        metadata = new FrameMetadata(messageId, lenType, payloadLength, instanceId);
        return true;
    }
}