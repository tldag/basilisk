using Autofac;
using Basilisk.Injection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace Basilisk.Tests.Injection
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
        public class Service2 : IHostedService
        {
            private readonly Data data;

            /// <summary />
            public Service2(Data data) { this.data = data; }

            /// <inheritdoc/>
            public async Task StartAsync(CancellationToken cancellationToken)
            {
                ++data.Service2Started;
                await Task.CompletedTask;
            }

            /// <inheritdoc/>
            public async Task StopAsync(CancellationToken cancellationToken)
            {
                ++data.Service2Stopped;
                await Task.CompletedTask;
            }
        }

        /// <summary>
        /// Test
        /// </summary>
        [TestMethod]
        public void Test()
        {
            Data data = new Data();

            IInjector injector = InjectorBuilder.Create()
                .AddInstance(data)
                .AddHostedService<Service1>()
                .AddHostedService<Service2>()
                .Build();
            
            injector.StartAsync().Wait();
            injector.StopAsync().Wait();

            Assert.AreEqual(1, data.Service1Started);
            Assert.AreEqual(1, data.Service1Stopped);
            Assert.AreEqual(1, data.Service2Started);
            Assert.AreEqual(1, data.Service2Stopped);
        }
    }
}
