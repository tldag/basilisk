using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Basilisk.Injection
{
    /// <summary>
    /// Injector interface.
    /// </summary>
    public interface IInjector : IContainer, IServiceProvider, ISupportRequiredService, IServiceProviderIsService, IHost
    {
    }
}
