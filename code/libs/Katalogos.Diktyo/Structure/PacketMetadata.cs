namespace Katalogos.Diktyo.Structure;

public record PacketMetadata(int MessageId,
    int LengthBytesCount,
    int PayloadLength,
    uint InstanceId);