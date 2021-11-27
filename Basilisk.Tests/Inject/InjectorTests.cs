using Autofac;
using Basilisk.Inject;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Basilisk.Tests.Inject
{
    /// <summary>
    /// InjectorTests
    /// </summary>
    [TestClass]
    public class InjectorTests
    {
        /// <summary>
        /// TestService.
        /// </summary>
        public class TestService { }

        /// <summary>
        /// Test.
        /// </summary>
        [TestMethod]
        public void Test()
        {
            IInjectorBuilder builder = InjectorBuilder
                .Create()
                .SetContentRoot(AppContext.BaseDirectory);

            builder.ConfigureServices((_, services) => services.AddSingleton<TestService>());

            using IInjector injector = builder.Build();

            IServiceProvider serviceProvider1 = injector.Resolve<IServiceProvider>();
            IHost host1 = injector.Resolve<IHost>();

            IServiceProvider? serviceProvider2 = injector.GetService<IServiceProvider>();
            IHost? host2 = injector.GetService<IHost>();

            Assert.IsTrue(ReferenceEquals(serviceProvider1, serviceProvider2));
            Assert.IsTrue(ReferenceEquals(host1, host2));

            IServiceProvider? serviceProvider3 = serviceProvider1.GetService<IServiceProvider>();
            IHost? host3 = serviceProvider1.GetService<IHost>();

            Assert.IsTrue(ReferenceEquals(serviceProvider1, serviceProvider3));
            Assert.IsTrue(ReferenceEquals(host1, host3));

            HostBuilderContext hostBuilderContext = injector.Resolve<HostBuilderContext>();
            IConfiguration configuration = injector.Resolve<IConfiguration>();
            TestService testService = injector.Resolve<TestService>();

            Assert.IsNotNull(hostBuilderContext);
            Assert.IsNotNull(configuration);
            Assert.IsNotNull(testService);
        }
    }
}
