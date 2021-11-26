namespace Basilisk.Injection.Configuration
{
    /// <summary>
    /// IConfigFactory
    /// </summary>
    public interface IConfigFactory
    {
        /// <summary>
        /// Host configuration.
        /// </summary>
        public IHostConfig CreateHostConfig();

        /// <summary>
        /// Application specific configuration.
        /// </summary>
        public IAppConfig CreateAppConfig();

        /// <summary>
        /// Logger configuration.
        /// </summary>
        public ILogConfig CreateLogConfig();

        /// <summary>
        /// Autofac configuration
        /// </summary>
        public IAutofacConfig CreateAutofacConfig();

        /// <summary>
        /// Host service callbacks.
        /// </summary>
        public IHostServices CreateHostServices();
    }
}
