using Katalogos.Seiriopoiisi.Cache.Serializer;

namespace Katalogos.Seiriopoiisi;

public class Serializer
{
    public Span<byte> Serialize<TInput>(TInput input)
    {
        return SerializerCache<TInput>.Serialize(input);
    }
}