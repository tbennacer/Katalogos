using Katalogos.Diktyo.Structure;

namespace Katalogos.Diktyo.Framing.Reader;

public interface IFrameUnpacker
{
    bool TryUnpack<TMessage>(Frame frame, out TMessage? message) where TMessage : INetworkMessage;
}