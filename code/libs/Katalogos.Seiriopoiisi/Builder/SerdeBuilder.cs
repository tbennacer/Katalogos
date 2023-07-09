using Katalogos.Seiriopoiisi.Builder.Deserializer;
using Katalogos.Seiriopoiisi.Builder.Serializer;
using Katalogos.Seiriopoiisi.Storage;

namespace Katalogos.Seiriopoiisi.Builder;

public class SerdeBuilder
{
    private readonly SerializerBuilder _serializerBuilder;
    private readonly DeserializerBuilder _deserializerBuilder;

    public SerdeBuilder(SerializerBuilder serializerBuilder, DeserializerBuilder deserializerBuilder)
    {
        _serializerBuilder = serializerBuilder;
        _deserializerBuilder = deserializerBuilder;
    }

    public void Register<TInput>(ISerializerStorage<TInput> storage)
    {
        _serializerBuilder.Register(storage);
    }

    public void Register<TOutput>(IDeserializerStorage<TOutput> storage)
    {
        _deserializerBuilder.Register(storage);
    }

    public void Register<TInput, TOutput>(ISerdeStorage<TInput, TOutput> storage)
    {
        _serializerBuilder.Register(storage);
        _deserializerBuilder.Register(storage);
    }
}