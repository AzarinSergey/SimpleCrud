using Gis.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Moedi.Data.Ef;
using Gis.Model.EntityConfig;

namespace Gis.Model
{
    public class GisDbContext : MoediDbContext
    {
        protected override string ConnectionName => "SqlConnection";
        protected override string Schema => "Gis";

        public DbSet<Airport> Airport { get; set; }
        public DbSet<GisDataRequestLog> GisDataRequestLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AirportTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GisDataRequestLogTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConnection(DbContextOptionsBuilder b, string connectionString)
        {
            b.UseNpgsql(connectionString);
        }
    }
}
