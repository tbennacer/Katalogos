using Katalogos.Diktyo.Behaviors;
using Katalogos.Diktyo.Framing;

namespace Katalogos.Diktyo.Dispatching;

internal class FrameDispatcher : IFrameDispatcher
{
    private readonly IDictionary<int, Handler> _handlers;

    public FrameDispatcher(IDictionary<int, Handler> handlers)
    {
        _handlers = handlers;
    }

    public ValueTask<DispatchResult> DispatchAsync(Frame frame, DispatchContext context)
    {
        if (context == default) throw new ArgumentNullException(nameof(context));

        return _handlers.TryGetValue(frame.Metadata.MessageId, out Handler? handler)
            ? handler(frame, context)
            : new ValueTask<DispatchResult>(DispatchResult.HandlerNotMapped);
    }
}