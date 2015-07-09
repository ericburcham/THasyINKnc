using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

using AsyncControllers.Models;
using AsyncControllers.Services;

namespace AsyncControllers.Controllers
{
    /// <summary>
    /// This is the old-school way, still around for MVC 3 compatibility.
    /// </summary>
    public class LocationController : AsyncController
    {
        /// <summary>
        /// Naming convention is [ActionName]Async
        /// </summary>
        public void GetDallasLibrariesAsync()
        {
            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(GetLibrariesNearDallas);
        }

        /// <summary>
        /// Naming convention is [ActionName]Completed
        /// </summary>
        public ViewResult GetDallasLibrariesCompleted(IEnumerable<GeoName> libraries)
        {
            return View(libraries);
        }

        private void GetLibrariesNearDallas()
        {
            var service = new LocationService();
            var dallasLibraries = service.GetDallasLibrariesInline();
            this.AsyncManager.Parameters["libraries"] = dallasLibraries;
            this.AsyncManager.OutstandingOperations.Decrement();
        }
    }

    ///// <summary>
    ///// This is the new sauce.
    ///// </summary>
    //public class LocationController : Controller
    //{
    //    public async Task<ViewResult> GetDallasLibraries()
    //    {
    //        var libraries = await GetLibrariesNearDallas();
    //        return View(libraries);
    //    }

    //    private Task<IEnumerable<GeoName>> GetLibrariesNearDallas()
    //    {
    //        var service = new LocationService();
    //        return Task.Factory.StartNew(() => service.GetDallasLibrariesInline());
    //    }
    //}
}