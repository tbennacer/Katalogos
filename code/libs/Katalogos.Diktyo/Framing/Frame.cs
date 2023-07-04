using System.Buffers;

namespace Katalogos.Diktyo.Framing;

public record Frame(FrameMetadata Metadata, ReadOnlySequence<byte> Payload);