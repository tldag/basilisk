using Autofac;

namespace Basilisk.Injection.Support
{
    /// <summary>
    /// IInjectorBuilderContext
    /// </summary>
    public interface IInjectorBuilderContext
    {
        /// <summary>
        /// The embedded <see cref="ContainerBuilder"/>.
        /// </summary>
        public ContainerBuilder ContainerBuilder { get; }
    }
}
