using Katalogos.Diktyo.Dispatching;
using Katalogos.Diktyo.Structure;

namespace Katalogos.Diktyo.Behaviors;

public abstract class MessageHandler<TMessage> where TMessage : class, INetworkMessage
{
    private DispatchContext? _dispatchContext;
    private TMessage? _networkMessage;

    public DispatchContext? DispatchContext
    {
        get { return _dispatchContext; }
        set { Interlocked.CompareExchange(ref _dispatchContext, value, null); }
    }

    public TMessage? NetworkMessage
    {
        get { return _networkMessage; }
        set { Interlocked.CompareExchange(ref _networkMessage, value, null); }
    }

    public void Setup(DispatchContext context, TMessage message)
    {
        DispatchContext = context;
        NetworkMessage = message;
    }

    public async ValueTask<HandlerResult> ProcessAsync(CancellationToken cancellationToken = default)
    {
        HandlerResult result = await HandleAsync(cancellationToken).ConfigureAwait(false);
        return result;
    }

    public abstract ValueTask<bool> CanProcessAsync();

    protected abstract ValueTask<HandlerResult> HandleAsync(CancellationToken token = default);
}