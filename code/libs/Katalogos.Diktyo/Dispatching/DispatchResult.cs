﻿using Katalogos.Diktyo.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Katalogos.Diktyo.Dispatching;

internal enum DispatchResult
{
    Success,

    /// <summary>
    ///     When the <see cref="MessageHandler{TMessage}" /> HandleAsync unsuccessfully handle the message.
    /// </summary>
    Failed,

    /// <summary>
    ///     When couldn't read message from payload.
    /// </summary>
    InvalidFrame,

    /// <summary>
    ///     When the <see cref="MessageHandler{TMessage}" /> CanProcess predicate failed.
    /// </summary>
    PredicateFailed,

    /// <summary>
    ///     When the message to handle wasn't mapped to an identifier during
    ///     behaviors configuration.
    /// </summary>
    HandlerNotMapped,

    /// <summary>
    ///     When the <see cref="MessageHandler{TMessage}" /> wasn't registered into the
    ///     <see cref="IServiceCollection" /> during behaviors configuration.
    /// </summary>
    HandlerNotRegistered
}