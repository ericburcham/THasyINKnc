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
            var libraries = await GetLibrariesNearDallas();
            return View(libraries);
        }

        private Task<IEnumerable<GeoName>> GetLibrariesNearDallas()
        {
            var service = new LocationService();
            return Task.Factory.StartNew(() => service.GetAll());
        }
    }
}