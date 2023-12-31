﻿using System.Buffers;

namespace Katalogos.Seiriopoiisi;

public class Serde : ISerde
{
    public Serializer Serializer { get; }

    public Deserializer Deserializer { get; }

    public Serde(Serializer serializer, Deserializer deserializer)
    {
        Serializer = serializer;
        Deserializer = deserializer;
    }

    public byte[] Serialize<TInput>(TInput input)
    {
        return Serializer.Serialize(input);
    }

    public bool TryDeserialize<TOutput>(in ReadOnlySequence<byte> buffer, out TOutput? output)
    {
        return Deserializer.TryDeserialize(buffer, out output);
    }
}