using System;
using System.Threading;
using Moedi.Core.Interfaces;
using Moedi.Core.Interfaces.Data.Access;

namespace Moedi.Ef
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