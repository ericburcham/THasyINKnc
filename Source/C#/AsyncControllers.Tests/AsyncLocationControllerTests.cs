using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using AsyncControllers.Controllers;
using AsyncControllers.Models;

using FluentAssertions;

using NUnit.Framework;

namespace AsyncControllers.Tests
{
    [TestFixture]
    public class AsyncLocationControllerTests
    {
        [Test]
        public void GetDallasLibrariesAsyncShouldReturnResults()
        {
            var controller = new AsyncLocationController();

            var waitHandle = new AutoResetEvent(false);
            EventHandler eventHandler = (sender, eventArgs) => waitHandle.Set();
            controller.AsyncManager.Finished += eventHandler;

            controller.GetDallasLibrariesAsync();

            if (!waitHandle.WaitOne(100000, false))
            {
                
            }

            var librariesParameter = (IEnumerable<GeoName>)controller.AsyncManager.Parameters["libraries"];
            var libraries = librariesParameter.ToList();

            libraries.Should().NotBeNull();
            libraries.Count().Should().BeGreaterThan(0);
        }
    }
}
