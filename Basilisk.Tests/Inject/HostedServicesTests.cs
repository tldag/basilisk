using Autofac;
using Basilisk.Inject;
using Basilisk.Inject.Host;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace Basilisk.Tests.Inject
{
    /// <summary>
    /// HostedServicesTests
    /// </summary>
    [TestClass]
    public class HostedServicesTests
    {
        /// <summary />
        public class Data
        {
            /// <summary />
            public int Service1Started;

            /// <summary />
            public int Service1Stopped;

            /// <summary />
            public int Service2Started;

            /// <summary />
            public int Service2Stopped;

            /// <summary />
            public int Service2Executed;
        }

        /// <summary />
        public class Service1 : IHostedService
        {
            private readonly Data data;

            /// <summary />
            public Service1(Data data) { this.data = data; }

            /// <inheritdoc/>
            public async Task StartAsync(CancellationToken cancellationToken)
            {
                ++data.Service1Started;
                await Task.CompletedTask;
            }

            /// <inheritdoc/>
            public async Task StopAsync(CancellationToken cancellationToken)
            {
                ++data.Service1Stopped;
                await Task.CompletedTask;
            }
        }

        /// <summary />
        public class Service2 : BackgroundService
        {
            private readonly Data data;

            /// <summary />
            public Service2(Data data) { this.data = data; }

            /// <inheritdoc/>
            public override async Task StartAsync(CancellationToken cancellationToken)
            {
                ++data.Service2Started;
                await base.StartAsync(cancellationToken);
            }

            /// <inheritdoc/>
            public override async Task StopAsync(CancellationToken cancellationToken)
            {
                await base.StopAsync(cancellationToken);
                ++data.Service2Stopped;
            }

            /// <inheritdoc/>
            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                ++data.Service2Executed;
                await Task.Delay(20, stoppingToken);
            }
        }

        /// <summary>
        /// Test
        /// </summary>
        [TestMethod]
        public void Test()
        {
            Data data = new();

            using IInjector injector = InjectorBuilder.Create()
                .AddInstance(data)
                .AddHostedService<Service1>()
                .AddHostedService<Service2>()
                .Build();

            IHostedServices hostedServices = injector.Resolve<IHostedServices>();

            hostedServices.StartAsync().Wait();
            hostedServices.WaitAsync().Wait();
            hostedServices.StopAsync().Wait();

            Assert.AreEqual(1, data.Service1Started);
            Assert.AreEqual(1, data.Service1Stopped);
            Assert.AreEqual(1, data.Service2Started);
            Assert.AreEqual(1, data.Service2Stopped);
            Assert.AreEqual(1, data.Service2Executed);
        }
    }
}
