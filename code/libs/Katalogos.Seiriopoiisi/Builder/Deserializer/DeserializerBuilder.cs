using Katalogos.Seiriopoiisi.Cache.Deserializer;
using Katalogos.Seiriopoiisi.Storage;

namespace Katalogos.Seiriopoiisi.Builder.Deserializer;

public class DeserializerBuilder
{
    public DeserializerBuilder Register<TOutput>(IDeserializerStorage<TOutput> storage)
    {
        DeserializerCache<TOutput>.TryDeserialize = storage.TryDeserialize;
        return this;
    }
}