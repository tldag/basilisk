using Basilisk.Injection;
using Basilisk.SystemServices;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace Basilisk.Tests.SystemServices
{
    /// <summary>
    /// SystemServiceHostTests
    /// </summary>
    [TestClass]
    public class SystemServiceHostTests
    {
        /// <summary>
        /// 
        /// </summary>
        public class TestSystemService : SystemService
        {
        }

        /// <summary>
        /// Test
        /// </summary>
        [TestMethod]
        public void Test()
        {
            using IInjector injector = SystemServiceHost.Create<TestSystemService>("SystemServiceHostTests.Test").CreateInjector();

            CancellationTokenSource cts = new();
            CancellationToken stopToken = cts.Token;
            injector.Start();

            Task task = injector.WaitForShutdownAsync(stopToken);

            Task.Delay(100).Wait();
            cts.Cancel();
            task.Wait();
        }
    }
}
