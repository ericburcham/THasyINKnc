using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using AsyncControllers.Models;
using AsyncControllers.Services;

namespace AsyncControllers.Controllers
{
    public class LocationController : AsyncController
    {
        //public void GetDallasLibrariesAsync()
        //{
        //    AsyncManager.OutstandingOperations.Increment();
        //    Task.Factory.StartNew(GetLibrariesNearDallas);
        //}


        //private void GetLibrariesNearDallas()
        //{
        //    var service = new LocationService();
        //    var dallasLibraries = service.GetDallasLibrariesInline();
        //    AsyncManager.Parameters["libraries"] = dallasLibraries;
        //    AsyncManager.OutstandingOperations.Decrement();
        //}

        //public ViewResult GetDallasLibrariesCompleted(IEnumerable<GeoName> libraries)
        //{
        //    return View(libraries);
        //}

        public async Task<ViewResult> GetDallasLibraries()
        {
            var libraries = await GetLibrariesNearDallas();
            return View(libraries);
        }

        private Task<IEnumerable<GeoName>> GetLibrariesNearDallas()
        {
            var service = new LocationService();
            return Task.Factory.StartNew(() => service.GetDallasLibrariesInline());
        }
    }
}