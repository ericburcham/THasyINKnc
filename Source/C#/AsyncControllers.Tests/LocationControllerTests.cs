using System.Collections.Generic;
using System.Linq;

using AsyncControllers.Controllers;
using AsyncControllers.Models;

using FluentAssertions;

using NUnit.Framework;

namespace AsyncControllers.Tests
{
    [TestFixture]
    public class LocationControllerTests
    {
        [Test]
        public void GetDallasLibrariesShouldReturnResults()
        {
            var controller = new LocationController();
            var result = controller.GetDallasLibraries().Result;

            var libraries = ((IEnumerable<GeoName>)result.Model).ToList();
            libraries.Should().NotBeNull();
            libraries.Count().Should().BeGreaterThan(0);
        }
    }
}
