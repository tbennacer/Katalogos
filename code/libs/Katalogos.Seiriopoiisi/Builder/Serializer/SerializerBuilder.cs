using Katalogos.Seiriopoiisi.Cache.Serializer;
using Katalogos.Seiriopoiisi.Storage;

namespace Katalogos.Seiriopoiisi.Builder.Serializer;

public class SerializerBuilder
{
    public SerializerBuilder Register<TInput>(ISerializerStorage<TInput> storage)
    {
        if (SerializerCache<TInput>.Serialize is not null)
            throw new InvalidOperationException($"Serializer {nameof(storage)} already cached/defined");

        SerializerCache<TInput>.Serialize = storage.Serialize;

        return this;
    }
}