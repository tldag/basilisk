using Autofac;
using Basilisk.Injection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Basilisk.Tests.Injection
{
    /// <summary>
    /// LoggerTests
    /// </summary>
    [TestClass]
    public class LoggerTests
    {
        /// <summary>
        /// Test
        /// </summary>
        [TestMethod]
        public void Test()
        {
            using IInjector injector = InjectorBuilder.Create().Build();

            ILogger<ApplicationLifetime> logger = injector.Resolve<ILogger<ApplicationLifetime>>();
            IHostApplicationLifetime hal = injector.Resolve<IHostApplicationLifetime>();
        }
    }
}
