using System.IO.Pipelines;
using Katalogos.Diktyo.Structure;

namespace Katalogos.Diktyo.Framing.Writing;

public interface IFrameWriter
{
    ValueTask<FlushResult> WriteAsync<TMessage>(PipeWriter writer,
        TMessage message,
        PacketSender sender,
        CancellationToken token = default) where TMessage : INetworkMessage;
}