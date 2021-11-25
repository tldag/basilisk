namespace Basilisk.Injection.Support
{
    /// <summary>
    /// IInjectorBuilderFactory
    /// </summary>
    public interface IInjectorBuilderFactory
    {
        /// <summary>
        /// Creates the context used in the <see cref="InjectorBuilder"/>.
        /// </summary>
        /// <returns>The context.</returns>
        public IInjectorBuilderContext CreateContext();
    }
}
