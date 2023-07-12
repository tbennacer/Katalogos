using Katalogos.Diktyo.Framing.Writing;
using Microsoft.AspNetCore.Connections;

namespace Katalogos.Diktyo.Dispatching;

public record DispatchContext(IFrameWriter Writer, ConnectionContext Connection);