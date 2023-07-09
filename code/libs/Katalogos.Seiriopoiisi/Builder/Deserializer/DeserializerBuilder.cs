using Katalogos.Seiriopoiisi.Cache.Deserializer;
using Katalogos.Seiriopoiisi.Storage;

namespace Katalogos.Seiriopoiisi.Builder.Deserializer;

public class DeserializerBuilder
{
    public DeserializerBuilder Register<TOutput>(IDeserializerStorage<TOutput> storage)
    {
        if (DeserializerCache<TOutput>.TryDeserialize is not null)
            throw new InvalidOperationException($"Deserializer {nameof(storage)} already cached/defined");

        DeserializerCache<TOutput>.TryDeserialize = storage.TryDeserialize;

        return this;
    }
}