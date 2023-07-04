using System.Buffers;
using Katalogos.Diktyo.Structure;

namespace Katalogos.Diktyo.Framing.Reader;

public class FrameMetadataReader
{
    public bool TryRead(ref SequenceReader<byte> bufferReader, PacketOrigin origin, out FrameMetadata? metadata)
    {
        metadata = default;
        if (!bufferReader.TryReadBigEndian(out short messageId)) return false;
        if (!bufferReader.TryReadBigEndian(out int length)) return false;

        int instanceId = default;
        if (origin == PacketOrigin.Server && !bufferReader.TryReadBigEndian(out instanceId))
            return false;

        var payloadLength =
            length > ushort.MaxValue ? 3
            : length > byte.MaxValue ? 2
            : length > 0 ? 1
            : 0;

        metadata = new FrameMetadata(messageId, length, payloadLength, instanceId);
        return true;
    }
}