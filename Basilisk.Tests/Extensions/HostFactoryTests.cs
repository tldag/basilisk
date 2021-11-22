using Basilisk.Extensions;
using Basilisk.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Basilisk.Tests.Extensions
{
    /// <summary>
    /// HostFactoryTests
    /// </summary>
    [TestClass]
    public class HostFactoryTests
    {
        /// <summary>
        /// TestOptions
        /// </summary>
        public class TestOptions
        {
            /// <summary>
            /// Port
            /// </summary>
            [JsonPropertyName("port")] 
            public int Port { get; set; }
        }

        /// <summary>
        /// CustomSettings
        /// </summary>
        public class CustomSettings
        {
            /// <summary>
            /// Test settings
            /// </summary>
            [JsonPropertyName("test")]
            public TestOptions Test { get; set; } = new();
        }

        [RequiresUnreferencedCode("")]
        private static void CreateCustomSettings()
        {
            TestOptions testOptions = new() { Port = 80 };
            CustomSettings customSettings = new() { Test = testOptions };
            FileInfo file = new("HostFactoryTests.json");
            using FileStream stream = file.OpenWrite();
            JsonWriterOptions writerOptions = new() { Indented = true };
            using Utf8JsonWriter writer = new(stream, writerOptions);

            JsonSerializer.Serialize(writer, customSettings);
        }

        /// <summary>
        /// Test
        /// </summary>
        [TestMethod]
        public void Test()
        {
            IHost host = HostFactory.Create().Build();

            host.StartAsync().Wait();
            Task.Delay(100).Wait();
            host.StopAsync().Wait();
        }

        /// <summary>
        /// TestHostBuilder
        /// </summary>
        [TestMethod]
        [RequiresUnreferencedCode("")]
        public void TestHostBuilder()
        {
            CreateCustomSettings();

            HostBuilder builder = new();

            builder.ConfigureAppConfiguration((ctx, bld) =>
            {
                bld.AddJsonFile("HostFactoryTests.json");
            });

            IHost host = builder.Build();
            
            IOptions<HostOptions> options = host.GetRequiredService<IOptions<HostOptions>>();
            IConfiguration configuration = host.GetRequiredService<IConfiguration>();

            Debug.WriteLine($"ShutdownTimeout = {options.Value.ShutdownTimeout}");

            TestOptions testOptions = configuration.GetSection("test").Get<TestOptions>();

            Assert.AreEqual(80, testOptions.Port);
        }
    }
}
