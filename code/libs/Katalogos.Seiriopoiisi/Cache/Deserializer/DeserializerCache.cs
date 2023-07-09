namespace Katalogos.Seiriopoiisi.Cache.Deserializer;

internal static class DeserializerCache<TOutput>
{
    private static TryDeserializeDelegate<TOutput>? _tryDeserialize;

    internal static TryDeserializeDelegate<TOutput> TryDeserialize
    {
        get
        {
            return _tryDeserialize ??
                   throw new NotImplementedException(
                       $"Deserializer {nameof(DeserializerCache<TOutput>)} isn't defined/cached");
        }
        set
        {
            if (_tryDeserialize is not null)
                throw new InvalidOperationException(
                    $"Deserializer {nameof(DeserializerCache<TOutput>)} already cached/defined");

            _tryDeserialize = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}