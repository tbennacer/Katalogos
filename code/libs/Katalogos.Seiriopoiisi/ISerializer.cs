namespace Katalogos.Seiriopoiisi;

public interface ISerializer
{
    Span<byte> Serialize<TInput>(TInput input);
}