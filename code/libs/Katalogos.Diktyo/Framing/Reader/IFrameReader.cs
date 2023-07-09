using System.IO.Pipelines;

namespace Katalogos.Diktyo.Framing.Reader;

public interface IFrameReader
{
    ValueTask<Frame> ReadFrameAsync(PipeReader reader, CancellationToken cancellationToken);

    IAsyncEnumerable<Frame> ReadFramesAsync(PipeReader reader, CancellationToken cancellationToken);
}