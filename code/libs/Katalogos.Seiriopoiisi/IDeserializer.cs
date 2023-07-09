using System.Buffers;

namespace Katalogos.Seiriopoiisi;

public interface IDeserializer
{
    bool TryDeserialize<TOutput>(in ReadOnlySequence<byte> buffer, out TOutput? output);
}