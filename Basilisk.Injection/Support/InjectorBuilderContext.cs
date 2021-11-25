using Autofac;

namespace Basilisk.Injection.Support
{
    /// <summary>
    /// <see cref="IInjectorBuilderContext"/> default implementation.
    /// </summary>
    public class InjectorBuilderContext : IInjectorBuilderContext
    {
        /// <inheritdoc/>
        public ContainerBuilder ContainerBuilder { get; } = new();
    }
}
