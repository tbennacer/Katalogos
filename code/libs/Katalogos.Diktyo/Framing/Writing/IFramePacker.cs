using Katalogos.Diktyo.Structure;

namespace Katalogos.Diktyo.Framing.Writing;

public interface IFramePacker
{
    Frame Pack<TMessage>(TMessage message, PacketSender sender) where TMessage : INetworkMessage;
}