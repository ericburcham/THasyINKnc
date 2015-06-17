using System.Linq;

using AsyncControllers.Controllers.Api;

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
            var controller = new GeoNamesApiController();
            var results = controller.GetLibraries();
            results.Count().Should().BeGreaterThan(0);
        }
    }
}
