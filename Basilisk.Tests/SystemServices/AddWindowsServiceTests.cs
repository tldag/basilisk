﻿using Basilisk.SystemServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Basilisk.Injection;
using Microsoft.Extensions.Hosting;
using System;
using System.Runtime.InteropServices;

namespace Basilisk.Tests.SystemServices
{
    /// <summary>
    /// Mimicking <see cref="SystemServiceHost.AddWindowsService(IInjectorBuilder)"/>.
    /// </summary>
    [TestClass]
    public class AddWindowsServiceTests
    {
        /// <summary>
        /// Test
        /// </summary>
        [TestMethod]
        public void Test()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return;

            IInjectorBuilder builder = InjectorBuilder.Create();

            builder.UseContentRoot(AppContext.BaseDirectory);
        }
    }
}