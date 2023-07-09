using System.Buffers;
using Katalogos.Diktyo.Structure;
using Katalogos.Seiriopoiisi;

namespace Katalogos.Diktyo.Framing.Writing;

public class FramePacker : IFramePacker
{
    private static int _instanceId;

    private readonly ISerializer _serializer;

    public FramePacker(ISerializer serializer)
    {
        _serializer = serializer;
    }


    public Frame Pack<TMessage>(TMessage message, PacketSender sender) where TMessage : INetworkMessage
    {
        byte[] payload = _serializer.Serialize(message);

        int lengthBytesCount = payload.Length > ushort.MaxValue ? 3
            : payload.Length > byte.MaxValue ? 2
            : payload.Length > 0 ? 1
            : 0;

        FrameMetadata metadata = new(message.ProtocolId,
            lengthBytesCount,
            payload.Length,
            _instanceId);

        if (sender == PacketSender.Client)
            _instanceId += 1;

        return new Frame(metadata, new ReadOnlySequence<byte>(payload));
    }
}