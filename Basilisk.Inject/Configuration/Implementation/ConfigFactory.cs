namespace Basilisk.Inject.Configuration.Implementation
{
    /// <summary>
    /// Default <see cref="IConfigFactory"/> implementation.
    /// </summary>
    public class ConfigFactory : IConfigFactory
    {
        /// <summary>
        /// CreateHostConfig
        /// </summary>
        /// <returns></returns>
        public virtual IHostConfig CreateHostConfig()
            => new HostConfig();

        /// <summary>
        /// CreateAppConfig
        /// </summary>
        /// <returns></returns>
        public virtual IAppConfig CreateAppConfig()
            => new AppConfig();

        /// <summary>
        /// CreateLogConfig
        /// </summary>
        /// <returns></returns>
        public virtual ILogConfig CreateLogConfig()
            => new LogConfig();

        /// <summary>
        /// CreateAutofacConfig
        /// </summary>
        /// <returns></returns>
        public virtual IAutofacConfig CreateAutofacConfig()
            => new AutofacConfig();

        /// <summary>
        /// CreateHostServices
        /// </summary>
        /// <returns></returns>
        public virtual IHostServices CreateHostServices()
            => new HostServices();
    }
}
