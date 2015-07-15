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
    public class AsyncLocationController : AsyncController
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
            var dallasLibraries = service.GetAll();
            AsyncManager.Parameters["libraries"] = dallasLibraries;
            AsyncManager.OutstandingOperations.Decrement();
        }
    }
}