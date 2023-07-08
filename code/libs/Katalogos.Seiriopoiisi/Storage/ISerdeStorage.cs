namespace Katalogos.Seiriopoiisi.Storage;

public interface ISerdeStorage<in TInput, in TOutput> : ISerializerStorage<TInput>, IDeserializerStorage<TOutput>
{
}