using System.Buffers;

namespace Katalogos.Seiriopoiisi;

public class Serde
{
    private readonly Serializer _serializer;
    private readonly Deserializer _deserializer;

    public Serde(Serializer serializer, Deserializer deserializer)
    {
        _serializer = serializer;
        _deserializer = deserializer;
    }

    public Span<byte> Serialize<TInput>(TInput input)
    {
        return _serializer.Serialize(input);
    }

    public bool TryDeserialize<TOutput>(in ReadOnlySequence<byte> buffer, out TOutput? output)
    {
        return _deserializer.TryDeserialize(buffer, out output);
    }
}