namespace Katalogos.Seiriopoiisi.Cache.Deserializer;

internal static class DeserializerCache<TOutput>
{
    private static TryDeserializeDelegate<TOutput>? _tryDeserialize;

    public static TryDeserializeDelegate<TOutput>? TryDeserialize
    {
        get
        {
            return _tryDeserialize ??
                   throw new NotImplementedException(
                       $"Deserializer {nameof(DeserializerCache<TOutput>)} isn't defined/cached");
        }
        set { _tryDeserialize = value ?? throw new ArgumentNullException(nameof(value)); }
    }
}