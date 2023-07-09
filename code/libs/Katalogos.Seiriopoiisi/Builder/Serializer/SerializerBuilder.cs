using Katalogos.Seiriopoiisi.Cache.Serializer;
using Katalogos.Seiriopoiisi.Storage;

namespace Katalogos.Seiriopoiisi.Builder.Serializer;

public class SerializerBuilder
{
    public SerializerBuilder Register<TInput>(ISerializerStorage<TInput> storage)
    {
        SerializerCache<TInput>.Serialize = storage.Serialize;
        return this;
    }
}