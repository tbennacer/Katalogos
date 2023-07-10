using System.IO.Pipelines;
using Katalogos.Diktyo.Structure;

namespace Katalogos.Diktyo.Framing.Reader;

public interface IFrameReader
{
    ValueTask<Frame> ReadFrameAsync(PipeReader reader,
        PacketSender sender,
        CancellationToken cancellationToken);

    IAsyncEnumerable<Frame> ReadFramesAsync(PipeReader reader,
        PacketSender sender,
        CancellationToken cancellationToken);
}