using Katalogos.Seiriopoiisi.Cache.Deserializer;

namespace Katalogos.Seiriopoiisi.Storage;

public interface IDeserializerStorage<in TOutput>
{
    TryDeserializeDelegate<TOutput> TryDeserialize { get; }
}