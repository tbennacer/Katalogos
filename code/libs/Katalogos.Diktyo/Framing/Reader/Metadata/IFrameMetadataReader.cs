using System.Buffers;
using Katalogos.Diktyo.Structure;

namespace Katalogos.Diktyo.Framing.Reader.Metadata;

public interface IFrameMetadataReader
{
    bool TryRead(ref SequenceReader<byte> bufferReader, PacketOrigin origin, out FrameMetadata? metadata);
}