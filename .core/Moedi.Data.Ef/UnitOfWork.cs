﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moedi.Data.Core.Access;

namespace Moedi.Data.Ef
{
    public sealed class UnitOfWork<TContext> : IUow
        where TContext : MoediDbContext
    {
        private readonly TContext _dbContext;
        private readonly CancellationToken _token;
        private readonly IDbContextTransaction _transaction;

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

        Task IUow.Commit()
        {
            return _transaction?.CommitAsync(_token);
        }

        Task IUow.Rollback()
        {
            return _transaction?.RollbackAsync(_token);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _dbContext?.Dispose();
        }

        ICommandRepository<TEntity> ICommandRepositoryFactory.GetRepository<TEntity>()
        {
            return new Repository<TEntity>(_dbContext, _token);
        }

        IQueryRepository<TEntity> IQueryRepositoryFactory.GetRepository<TEntity>()
        {
            return new Repository<TEntity>(_dbContext, _token);
        }
    }
}