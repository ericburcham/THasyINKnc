using System;
using System.Data.Entity.Spatial;

namespace AsyncControllers.Models
{
    public class GeoName
    {
        public string AdminCode1 { get; set; }

        public string AdminCode2 { get; set; }

        public string AdminCode3 { get; set; }

        public string AdminCode4 { get; set; }

        public string AlternateCountryCode { get; set; }

        public string AlternateNames { get; set; }

        public string AsciiName { get; set; }

        public string CountryCode { get; set; }

        public DateTime? DateLastModified { get; set; }

        public int? DigitalElevationModel { get; set; }

        public int? Elevation { get; set; }

        public string FeatureClass { get; set; }

        public string FeatureCode { get; set; }

        public DbGeography GeoData { get; set; }

        public int? GeoNameId { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string Name { get; set; }

        public long? Population { get; set; }

        public string TimeZone { get; set; }
    }
}