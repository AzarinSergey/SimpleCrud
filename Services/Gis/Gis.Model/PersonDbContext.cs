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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AirportTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //connection string for migrations only 
            //dotnet-ef migrations add Initial --context GisDbContext
            //dotnet-ef database update
            //optionsBuilder.UseSqlServer("data source=(local)\\SQL2016;initial catalog=SimpleCrud;trusted_connection=true;");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
