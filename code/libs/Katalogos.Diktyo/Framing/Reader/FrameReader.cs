using Katalogos.Diktyo.Structure;

namespace Katalogos.Diktyo.Framing.Reader;

public class FrameReader
{
    public bool TryRead<TMessage>(Frame frame, out TMessage? message) where TMessage : INetworkMessage
    {
        message = default;

        /* DESERIALIZATION */
        
        return true;
    }
}