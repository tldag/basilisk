using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Autofac.Core;
using Autofac.Core.Lifetime;
using Autofac.Core.Resolving;
using Microsoft.Extensions.DependencyInjection;

namespace Basilisk.Injection.Inject

{
    /// <summary>
    /// Injector
    /// </summary>
    public class Injector : IInjector
    {
        /// <summary>
        /// The Autofac container.
        /// </summary>
        protected IContainer Container { get; }

        /// <summary>
        /// Constructs a new injector
        /// </summary>
        /// <param name="container">The Autofac container</param>
        public Injector(IContainer container)
        {
            Container = container;
        }

        // IDisposable

        /// <summary>
        /// Destructor.
        /// </summary>
        ~Injector()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        private void Dispose(bool _)
        {
            Container.Dispose();
        }

        // IAsyncDisposable

        /// <inheritdoc/>
        public async ValueTask DisposeAsync()
        {
            GC.SuppressFinalize(this);
            await DisposeAsyncCore();
            Dispose(false);
        }

        /// <inheritdoc/>
        protected virtual async ValueTask DisposeAsyncCore()
        {
            await Task.CompletedTask;
        }

        // IContainer

        /// <inheritdoc/>
        public DiagnosticListener DiagnosticSource => Container.DiagnosticSource;

        // ILifetimeScope

        /// <inheritdoc/>
        public IDisposer Disposer => Container.Disposer;

        /// <inheritdoc/>
        public object Tag => Container.Tag;

        /// <inheritdoc/>
        public event EventHandler<LifetimeScopeBeginningEventArgs> ChildLifetimeScopeBeginning
        {
            add { Container.ChildLifetimeScopeBeginning += value; }
            remove => Container.ChildLifetimeScopeBeginning -= value;
        }

        /// <inheritdoc/>
        public event EventHandler<LifetimeScopeEndingEventArgs> CurrentScopeEnding
        {
            add { Container.CurrentScopeEnding += value; }
            remove { Container.CurrentScopeEnding -= value; }
        }

        /// <inheritdoc/>
        public event EventHandler<ResolveOperationBeginningEventArgs> ResolveOperationBeginning
        {
            add { Container.ResolveOperationBeginning += value; }
            remove { Container.ResolveOperationBeginning -= value; }
        }

        /// <inheritdoc/>
        public ILifetimeScope BeginLifetimeScope()
            => Container.BeginLifetimeScope();

        /// <inheritdoc/>
        public ILifetimeScope BeginLifetimeScope(object tag)
            => Container.BeginLifetimeScope(tag);

        /// <inheritdoc/>
        public ILifetimeScope BeginLifetimeScope(Action<ContainerBuilder> configurationAction)
            => Container.BeginLifetimeScope(configurationAction);

        /// <inheritdoc/>
        public ILifetimeScope BeginLifetimeScope(object tag, Action<ContainerBuilder> configurationAction)
            => Container.BeginLifetimeScope(tag, configurationAction);

        // IComponentContext

        /// <inheritdoc/>
        public IComponentRegistry ComponentRegistry => Container.ComponentRegistry;

        /// <inheritdoc/>
        public object ResolveComponent(ResolveRequest request)
            => Container.ResolveComponent(request);

        // IServiceProvider

        /// <inheritdoc/>
        public object? GetService(Type serviceType)
            => Container.Resolve<IServiceProvider>().GetService(serviceType);

        // ISupportRequiredService

        /// <inheritdoc/>
        public object GetRequiredService(Type serviceType)
            => Container.Resolve<ISupportRequiredService>().GetRequiredService(serviceType);

        // IServiceProviderIsService

        /// <inheritdoc/>
        public bool IsService(Type serviceType)
            => Container.Resolve<IServiceProviderIsService>().IsService(serviceType);

        // IHost

        /// <inheritdoc/>
        public IServiceProvider Services => Container.Resolve<IServiceProvider>();

        /// <inheritdoc/>
        public Task StartAsync(CancellationToken cancellationToken = default)
            => Container.Resolve<IHost>().StartAsync(cancellationToken);

        /// <inheritdoc/>
        public Task StopAsync(CancellationToken cancellationToken = default)
            => Container.Resolve<IHost>().StopAsync(cancellationToken);
    }
}
