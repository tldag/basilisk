using Basilisk.Inject.Configuration;
using Basilisk.Inject.Configuration.Implementation;

namespace Basilisk.Inject.Implementation
{
    public partial class InjectorBuilder : Inject.InjectorBuilder
    {
        private IConfigFactory? configFactory = null;
        private IHostConfig? hostConfig = null;
        private IAppConfig? appConfig = null;
        private ILogConfig? logConfig = null;
        private IAutofacConfig? autofacConfig = null;
        private IHostServices? hostServices = null;

        /// <summary>
        /// ConfigFactory
        /// </summary>
        protected IConfigFactory ConfigFactory { get { return configFactory ??= CreateConfigFactory(); } }

        /// <inheritdoc/>
        public override IHostConfig HostConfig => hostConfig ??= ConfigFactory.CreateHostConfig();

        /// <inheritdoc/>
        override public IAppConfig AppConfig => appConfig ??= ConfigFactory.CreateAppConfig();

        /// <inheritdoc/>
        override public ILogConfig LogConfig => logConfig ??= ConfigFactory.CreateLogConfig();

        /// <inheritdoc/>
        public override IAutofacConfig AutofacConfig => autofacConfig ??= ConfigFactory.CreateAutofacConfig();

        /// <inheritdoc/>
        public override IHostServices HostServices => hostServices ??= ConfigFactory.CreateHostServices();

        /// <summary>
        /// CreateConfigFactory
        /// </summary>
        /// <returns></returns>
        protected virtual IConfigFactory CreateConfigFactory()
            => new ConfigFactory();
    }
}
