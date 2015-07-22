using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

using AsyncControllers.Models;

namespace AsyncControllers.Services
{
    public class LocationService
    {
        public Task<IEnumerable<GeoName>> GetAllAsync()
        {
            return Task.Factory.StartNew(() => GetAll());
        }

        public IEnumerable<GeoName> GetAll()
        {
            // Thread.Sleep(5000);
         
            var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var dataContext = new GeoDataContext(connectionString);

            var dallasTx = dataContext.Set<GeoName>().Single(name => name.GeoNameId == GEO_NAME_ID_FOR_DALLAS_TX);
            var targetLatitude = dallasTx.Latitude.GetValueOrDefault(32.78306);
            var milesPerDegreeLongitude = MILES_PER_DEGREE_LATITUDE * Math.Cos(targetLatitude * (Math.PI / 180));
            const double LatitudeDeltatToMaxDistance = MAX_DISTANCE / MILES_PER_DEGREE_LATITUDE;
            var longitudeDeltaToMaxDistance = MAX_DISTANCE / milesPerDegreeLongitude;

            var geoNamesSet = dataContext.Set<GeoName>();
            var q = from m in geoNamesSet
                    where m.GeoNameId != dallasTx.GeoNameId                                             // We don't want the target in the list of results
                          && m.FeatureClass == LIBRARY_FEATURE_CLASS                                    // Limit by feature class
                          && m.FeatureCode == LIBRARY_FEATURE_CODE                                      // Limit by the "Library" feature code
                          && (dallasTx.Latitude >= m.Latitude - LatitudeDeltatToMaxDistance
                              && dallasTx.Latitude <= m.Latitude + LatitudeDeltatToMaxDistance)         // Limit by latitude "Box"
                          && (dallasTx.Longitude >= m.Longitude - longitudeDeltaToMaxDistance
                              && dallasTx.Longitude <= m.Longitude + longitudeDeltaToMaxDistance)       // Limit by longitude "Box"
                          && (dallasTx.GeoData.Distance(m.GeoData) / METERS_PER_MILE) <= MAX_DISTANCE   // Limit by EXACT distance
                    select m;

            return q.OrderBy(match => match.AsciiName).ToList();
        }

        private const string LIBRARY_FEATURE_CLASS = "S";

        private const string LIBRARY_FEATURE_CODE = "LIBR";

        private const int GEO_NAME_ID_FOR_DALLAS_TX = 4646211;

        private const int MAX_DISTANCE = 100;

        private const double METERS_PER_MILE = 1609.344;

        private const double MILES_PER_DEGREE_LATITUDE = 69.04;
    }
}