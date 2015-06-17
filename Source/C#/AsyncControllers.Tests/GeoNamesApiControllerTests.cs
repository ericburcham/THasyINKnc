using System.Collections.Generic;
using System.Linq;

using AsyncControllers.Controllers;
using AsyncControllers.Models;

using FluentAssertions;

using NUnit.Framework;

namespace AsyncControllers.Tests
{
    [TestFixture]
    public class GeoNamesApiControllerTests
    {
        [Test]
        public void GetLibrariesShouldReturnResults()
        {
            var controller = new LocationController();
            var result = controller.GetDallasLibraries().Result;

            // ReSharper disable once AssignNullToNotNullAttribute
            var libraries = (result.Model as IEnumerable<GeoName>).ToList();
            libraries.Should().NotBeNull();
            libraries.Count().Should().BeGreaterThan(0);
        }
    }
}
