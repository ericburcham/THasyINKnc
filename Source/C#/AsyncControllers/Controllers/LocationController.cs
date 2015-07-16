using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

using AsyncControllers.Models;
using AsyncControllers.Services;

namespace AsyncControllers.Controllers
{
    /// <summary>
    /// This is the new sauce.
    /// </summary>
    public class LocationController : Controller
    {
        public async Task<ViewResult> GetDallasLibraries()
        {
            var libraries = await this.GetDallasLibrariesAsync();
            return View(libraries);
        }

        private async Task<IEnumerable<GeoName>> GetDallasLibrariesAsync()
        {
            var service = new LocationService();
            return await service.GetAllAsync().ConfigureAwait(false);
        }
    }
}