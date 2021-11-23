using Microsoft.Extensions.DependencyInjection;
using System;

namespace Basilisk.Injection
{
    /// <summary>
    /// Injector interface.
    /// </summary>
    public interface IInjector : IServiceProvider, ISupportRequiredService, IServiceProviderIsService
    {
    }
}
