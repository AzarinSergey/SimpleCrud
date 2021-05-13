﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Moedi.Data.Ef
{
    public abstract class MoediDbContext : DbContext
    {
        protected abstract string Schema { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ValidateEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ValidateEntities()
        {
            var entities = ChangeTracker.Entries()
                .Where(e => new[] { EntityState.Added, EntityState.Modified }.Contains(e.State))
                .Select(e => e.Entity);

            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(x =>
            {
                x.MigrationsHistoryTable("__EFMigrationsHistory", Schema);
            });
        }
    }
}