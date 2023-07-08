namespace Katalogos.Seiriopoiisi.Cache.Serializer;

public delegate Span<byte> SerializeDelegate<in TInput>(TInput input);