namespace Katalogos.Seiriopoiisi;

public interface ISerializer
{
    byte[] Serialize<TInput>(TInput input);
}