using Autofac;
using Basilisk.Injection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Basilisk.Tests.Injection
{
    /// <summary>
    /// InjectorTests
    /// </summary>
    [TestClass]
    public class InjectorTests
    {
        /// <summary>
        /// Test.
        /// </summary>
        [TestMethod]
        public void Test()
        {
            using IInjector injector = InjectorBuilder.Create().Build();

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
        }
    }
}
