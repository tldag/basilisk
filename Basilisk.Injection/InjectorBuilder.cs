using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Basilisk.Injection
{
    /// <summary>
    /// Injector builder.
    /// </summary>
    public class InjectorBuilder : ServiceCollection
    {
        /// <summary>
        /// The Autofac builder.
        /// </summary>
        protected ContainerBuilder Builder { get; } = new();

        /// <summary>
        /// Builds the injector.
        /// </summary>
        /// <returns>The injector.</returns>
        public virtual IInjector Build()
        {
            Builder.Populate(this);

            IContainer container = Builder.Build();

            return new Injector(container);
        }

        /// <summary>
        /// Creates a new builder.
        /// </summary>
        /// <returns>A new builder.</returns>
        public static InjectorBuilder Create() => new();
    }
}
