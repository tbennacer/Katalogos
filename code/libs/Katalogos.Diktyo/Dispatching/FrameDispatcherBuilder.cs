using Katalogos.Diktyo.Behaviors;
using Katalogos.Diktyo.Framing;
using Katalogos.Diktyo.Framing.Reader;
using Katalogos.Diktyo.Structure;
using Microsoft.Extensions.DependencyInjection;

namespace Katalogos.Diktyo.Dispatching;

internal class FrameDispatcherBuilder
{
    private readonly IDictionary<int, Handler> _handlers;
    private readonly IFrameUnpacker _frameUnpacker;
    private readonly IServiceProvider _serviceProvider;

    public FrameDispatcherBuilder(IFrameUnpacker frameUnpacker, IServiceProvider serviceProvider)
    {
        _frameUnpacker = frameUnpacker;
        _serviceProvider = serviceProvider;
        _handlers = new Dictionary<int, Handler>();
    }

    public FrameDispatcherBuilder Register<TMessage>(int id) where TMessage : class, INetworkMessage, new()
    {
        _handlers[id] = Handler;
        return this;

        async ValueTask<DispatchResult> Handler(Frame frame, DispatchContext context)
        {
            if (!_frameUnpacker.TryUnpack(frame, out TMessage? message))
                return DispatchResult.InvalidFrame;

            using IServiceScope scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetService<MessageHandler<TMessage>>();

            if (handler == default) return DispatchResult.HandlerNotRegistered;
            handler.Setup(context, message!);

            if (!await handler.CanProcessAsync().ConfigureAwait(false))
                return DispatchResult.PredicateFailed;

            CancellationToken token = context.Connection.ConnectionClosed;

            HandlerResult result =
                await handler.ProcessAsync(token).ConfigureAwait(false);

            if (!result.SuccessfullyHandled)
                return DispatchResult.Failed;

            await result.action.ExecuteAsync(context).ConfigureAwait(false);

            return DispatchResult.Success;
        }
    }

    public FrameDispatcher Build()
    {
        return new FrameDispatcher(_handlers);
    }
}