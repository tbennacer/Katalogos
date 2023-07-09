using System.Buffers;
using Katalogos.Diktyo.Structure;

namespace Katalogos.Diktyo.Framing.Reader.Metadata;

public interface IFrameMetadataReader
{
    bool TryRead(ref SequenceReader<byte> bufferReader, PacketSender sender, out FrameMetadata? metadata);
}