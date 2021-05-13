using Microsoft.EntityFrameworkCore;
using Moedi.Data.Ef;
using Person.Model.EntityConfig;

namespace Person.Model
{
    public class PersonDbContext : MoediDbContext
    {
        protected override string ConnectionName => "SqlConnection";
        protected override string Schema => "Person";

        public DbSet<Entity.Person> Person { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //connection string for migrations only 
            //dotnet-ef migrations add Initial --context PersonDbContext
            //dotnet-ef database update
            //optionsBuilder.UseSqlServer("data source=(local)\\SQL2016;initial catalog=SimpleCrud;trusted_connection=true;");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
