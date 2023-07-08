using Katalogos.Seiriopoiisi.Cache.Serializer;

namespace Katalogos.Seiriopoiisi.Storage;

public interface ISerializerStorage<in TInput>
{
    SerializeDelegate<TInput> Serialize { get; }
}