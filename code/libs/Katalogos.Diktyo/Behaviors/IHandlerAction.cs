using Katalogos.Diktyo.Dispatching;

namespace Katalogos.Diktyo.Behaviors;

public interface IHandlerAction
{
    ValueTask ExecuteAsync(DispatchContext context);
}