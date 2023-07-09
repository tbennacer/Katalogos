using System.Buffers;

namespace Katalogos.Seiriopoiisi.Cache.Deserializer;

public delegate bool TryDeserializeDelegate<TOutput>(in ReadOnlySequence<byte> buffer, out TOutput output);