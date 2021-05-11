using System;
using System.Threading;
using Moedi.Data.Core.Access;

namespace Moedi.Data.Ef
{
    public class UowFactory<TDbContext> : IUowFactory
        where TDbContext : MoediDbContext, new()
    {
        IUow IUowFactory.CreateUnitOfWork(Guid? transactionUuid, CancellationToken token)
        {
            var dbContext = new TDbContext();

            return new UnitOfWork<TDbContext>(dbContext, transactionUuid.HasValue, token);
        }
    }
}