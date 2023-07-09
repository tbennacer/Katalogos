using Katalogos.Diktyo.Structure;
using Katalogos.Seiriopoiisi;

namespace Katalogos.Diktyo.Framing.Reader;

public class FrameUnpacker : IFrameUnpacker
{
    private readonly IDeserializer _deserializer;

    public FrameUnpacker(IDeserializer deserializer)
    {
        _deserializer = deserializer;
    }

    public bool TryUnpack<TMessage>(Frame frame, out TMessage? message) where TMessage : INetworkMessage
    {
        return _deserializer.TryDeserialize(frame.Payload, out message);
    }
}