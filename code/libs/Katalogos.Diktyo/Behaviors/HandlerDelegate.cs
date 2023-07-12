using Katalogos.Diktyo.Dispatching;
using Katalogos.Diktyo.Framing;

namespace Katalogos.Diktyo.Behaviors;

internal delegate ValueTask<DispatchResult> Handler(Frame frame, DispatchContext context);