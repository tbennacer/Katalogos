using Katalogos.Seiriopoiisi.Cache.Deserializer;

namespace Katalogos.Seiriopoiisi.Storage;

public interface IDeserializerStorage<TOutput>
{
    TryDeserializeDelegate<TOutput> TryDeserialize { get; }
}