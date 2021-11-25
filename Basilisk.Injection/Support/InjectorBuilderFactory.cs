namespace Basilisk.Injection.Support
{
    /// <summary>
    /// <see cref="IInjectorBuilderFactory"/> default implementation.
    /// </summary>
    public class InjectorBuilderFactory : IInjectorBuilderFactory
    {
        /// <inheritdoc/>
        public IInjectorBuilderContext CreateContext()
            => new InjectorBuilderContext();
    }
}
