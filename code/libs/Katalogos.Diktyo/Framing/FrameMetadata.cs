namespace Katalogos.Diktyo.Framing;

public record FrameMetadata(int MessageId,
    int LengthBytesCount,
    int PayloadLength,
    int InstanceId);