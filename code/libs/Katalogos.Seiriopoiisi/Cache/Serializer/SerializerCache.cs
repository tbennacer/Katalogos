namespace Katalogos.Seiriopoiisi.Cache.Serializer;

internal static class SerializerCache<TInput>
{
    private static SerializeDelegate<TInput>? _serialize;

    internal static SerializeDelegate<TInput> Serialize
    {
        get
        {
            return _serialize ??
                   throw new NotImplementedException(
                       $"Serializer {nameof(SerializerCache<TInput>)} isn't defined/cached");
        }
        set
        {
            if (_serialize is not null)
                throw new InvalidOperationException(
                    $"Deserializer {nameof(SerializerCache<TInput>)} already cached/defined");

            _serialize = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}