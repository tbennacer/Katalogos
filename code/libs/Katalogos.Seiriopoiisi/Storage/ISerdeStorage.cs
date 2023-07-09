namespace Katalogos.Seiriopoiisi.Storage;

public interface ISerdeStorage<in TInput, TOutput> :
    ISerializerStorage<TInput>, IDeserializerStorage<TOutput> { }