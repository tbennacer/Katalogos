using Katalogos.Diktyo.Framing;

namespace Katalogos.Diktyo.Dispatching;

internal interface IFrameDispatcher
{
    ValueTask<DispatchResult> DispatchAsync(Frame frame, DispatchContext context);
}