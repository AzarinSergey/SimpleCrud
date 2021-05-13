using Microsoft.EntityFrameworkCore;
using Moedi.Data.Ef;
using Person.Model;
using System;

namespace Projection.Domain
{
    public class ProjectionDbContext : MoediDbContext
    {
        protected override string ConnectionName => "SqlConnection";

        protected override string Schema => "dbo";

        private static readonly Type[] ContextTypes =
        {
            typeof(PersonDbContext)
        };


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RegisterEntities(ContextTypes);
        }
    }
}
