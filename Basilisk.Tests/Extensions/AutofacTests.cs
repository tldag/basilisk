using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace Basilisk.Tests.Extensions
{
    /// <summary>
    /// AutofacTests
    /// </summary>
    [TestClass]
    public class AutofacTests
    {
        /// <summary>
        /// ITestData
        /// </summary>
        public interface ITestData
        {
            /// <summary>
            /// Value
            /// </summary>
            public int Value { get; }

            /// <summary>
            /// Increment
            /// </summary>
            public void Increment();
        }

        /// <summary>
        /// 
        /// </summary>
        public class TestData : ITestData
        {
            private int value;

            /// <inheritdoc/>
            public int Value => value;

            /// <inheritdoc/>
            public void Increment()
            {
                ++value;
            }
        }

        /// <summary>
        /// IFooService
        /// </summary>
        public interface IFooService : IHostedService { }

        /// <summary>
        /// TestService
        /// </summary>
        public class FooService : IFooService
        {
            private readonly ITestData testData;

            /// <summary>
            /// TestService
            /// </summary>
            /// <param name="testData"></param>
            public FooService(ITestData testData)
            {
                this.testData = testData;
            }

            /// <inheritdoc/>
            public async Task StartAsync(CancellationToken cancellationToken)
            {
                testData.Increment();
                await Task.CompletedTask;
            }

            /// <inheritdoc/>
            public async Task StopAsync(CancellationToken cancellationToken)
            {
                testData.Increment();
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// IBarService
        /// </summary>
        public interface IBarService : IHostedService { }

        /// <summary>
        /// BarService
        /// </summary>
        public class BarService : IBarService
        {
            private readonly ITestData testData;

            /// <summary>
            /// TestService
            /// </summary>
            /// <param name="testData"></param>
            public BarService(ITestData testData)
            {
                this.testData = testData;
            }

            /// <inheritdoc/>
            public async Task StartAsync(CancellationToken cancellationToken)
            {
                testData.Increment();
                await Task.CompletedTask;
            }

            /// <inheritdoc/>
            public async Task StopAsync(CancellationToken cancellationToken)
            {
                testData.Increment();
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// Test
        /// </summary>
        [TestMethod]
        public void Test()
        {
            ServiceCollection services = new();

            services.AddSingleton<IFooService, FooService>();
            services.AddSingleton<IBarService, BarService>();

            ContainerBuilder builder = new();

            builder.RegisterType<TestData>().As<ITestData>().SingleInstance();

            builder.Populate(services);

            using IContainer container = builder.Build();
            ITestData testData = container.Resolve<ITestData>();
            IFooService fooService = container.Resolve<IFooService>();
            IFooService fooService2 = container.Resolve<IFooService>();
            IBarService barService = container.Resolve<IBarService>();

            Assert.IsNotNull(fooService);
            Assert.IsNotNull(barService);

            Assert.IsTrue(ReferenceEquals(fooService, fooService2));

            using CancellationTokenSource cts = new();
            CancellationToken token = cts.Token;

            fooService.StartAsync(token);
            fooService.StopAsync(token);

            barService.StartAsync(token);
            barService.StopAsync(token);

            Assert.AreEqual(4, testData.Value);
        }
    }
}
