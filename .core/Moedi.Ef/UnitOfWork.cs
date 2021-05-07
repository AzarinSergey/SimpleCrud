using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moedi.Core.Interfaces.Data.Access;
using System.Threading;
using Moedi.Core.Interfaces;

namespace Moedi.Ef
{
    public sealed class UnitOfWork<TContext> : IUow
        where TContext : MoediDbContext
    {
        private readonly TContext _dbContext;
        private CancellationToken _token;
        private IDbContextTransaction _transaction;

        public UnitOfWork(TContext dbContext, bool transaction, CancellationToken token)
        {
            _dbContext = dbContext;
            _token = token;

            if (transaction)
            {
                _transaction = _dbContext.Database.BeginTransaction();
            }
            else
            {
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
                _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            }
        }

        void IUow.Commit()
        {
            _transaction?.Commit();
        }

        void IUow.Rollback()
        {
            _transaction?.Rollback();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _dbContext?.Dispose();
        }

        ICommandRepository<TEntity> ICommandRepositoryFactory.GetRepository<TEntity>()
        {
            throw new System.NotImplementedException();
        }

        IQueryRepository<TEntity> IQueryRepositoryFactory.GetRepository<TEntity>()
        {
            throw new System.NotImplementedException();
        }
    }
}