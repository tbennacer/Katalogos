using System.Buffers;

namespace Katalogos.Seiriopoiisi.Cache.Deserializer;

public delegate bool TryDeserializeDelegate<in TOutput>(ReadOnlySequence<byte> buffer, TOutput output);