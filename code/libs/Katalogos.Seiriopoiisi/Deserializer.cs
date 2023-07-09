using System.Buffers;
using Katalogos.Seiriopoiisi.Cache.Deserializer;

namespace Katalogos.Seiriopoiisi;

public class Deserializer
{
    public bool TryDeserialize<TOutput>(in ReadOnlySequence<byte> buffer, out TOutput? output)
    {
        return DeserializerCache<TOutput>.TryDeserialize(in buffer, out output);
    }
}