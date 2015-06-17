using System.Data.Entity;

using AsyncControllers.Models;

namespace AsyncControllers
{
    public class GeoDataContext : DbContext
    {
        public GeoDataContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeoName>();
        }
    }
}