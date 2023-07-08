using Katalogos.Diktyo.Structure;

namespace Katalogos.Diktyo.Framing.Writing.Metadata;

public interface IFrameMetadataWriter
{
    bool TryWrite(Span<byte> buffer, FrameMetadata metadata, PacketOrigin origin);
}