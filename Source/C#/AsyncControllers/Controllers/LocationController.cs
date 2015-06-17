using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using AsyncControllers.Models;
using AsyncControllers.Services;

namespace AsyncControllers.Controllers
{
    public class LocationController : AsyncController
    {
        public void GetDallasLibrariesAsync()
        {
            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(GetLibraries);
        }

        private void GetLibraries()
        {
            var service = new LocationService();
            var dallasLibraries = service.GetDallasLibrariesInline();
            this.AsyncManager.Parameters["libraries"] = dallasLibraries;
            this.AsyncManager.OutstandingOperations.Decrement();
        }

        public ActionResult GetDallasLibrariesCompleted(IEnumerable<GeoName> libraries)
        {
            return View(libraries);
        }
    }
}