using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Basilisk.Tests.Core
{
    /// <summary>
    /// JsonTests
    /// </summary>
    [TestClass]
    public class NewtonsoftTests
    {
        /// <summary>
        /// TestConfig
        /// </summary>
        public class TestConfig
        {
            /// <summary>
            /// Port
            /// </summary>
            [JsonProperty("port")] 
            public int Port { get; set; } = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public class AppSettings
        {
            /// <summary>
            /// TestConfig
            /// </summary>
            [JsonProperty("test")]
            public TestConfig TestConfig { get; set; } = new();
        }

        /// <summary>
        /// Test
        /// </summary>
        [TestMethod]
        public void Test()
        {
            AppSettings appSettings = new AppSettings();

            appSettings.TestConfig.Port = 81;

            string json = JsonConvert.SerializeObject(appSettings, Formatting.Indented);
            AppSettings? appSettings2 = JsonConvert.DeserializeObject<AppSettings>(json);

            Assert.IsNotNull(appSettings2);
            Assert.AreEqual(appSettings.TestConfig.Port, appSettings2.TestConfig.Port);
        }
    }
}
