namespace Katalogos.Diktyo.Behaviors;

public record HandlerResult(IHandlerAction action, bool SuccessfullyHandled);