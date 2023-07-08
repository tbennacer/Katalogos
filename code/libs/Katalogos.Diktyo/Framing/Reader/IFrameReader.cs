using Katalogos.Diktyo.Structure;

namespace Katalogos.Diktyo.Framing.Reader;

public interface IFrameReader
{
    bool TryRead<TMessage>(Frame frame, out TMessage? message) where TMessage : INetworkMessage;
}