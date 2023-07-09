namespace Katalogos.Seiriopoiisi.Cache.Serializer;

internal static class SerializerCache<TInput>
{
    private static SerializeDelegate<TInput>? _serialize;

    public static SerializeDelegate<TInput>? Serialize
    {
        get
        {
            return _serialize ??
                   throw new NotImplementedException(
                       $"Serializer {nameof(SerializerCache<TInput>)} isn't defined/cached");
        }
        set { _serialize = value ?? throw new ArgumentNullException(nameof(value)); }
    }
}