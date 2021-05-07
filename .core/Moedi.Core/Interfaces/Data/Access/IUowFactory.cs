using System;
using System.Threading;

namespace Moedi.Core.Interfaces.Data.Access
{
    public interface IUowFactory
    {
        IUow CreateUnitOfWork(Guid? transactionUuid, CancellationToken token = default);
    }
}