using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedi.Core.Interfaces.Data.Access;
using Moedi.Core.Interfaces.Data.Entity;

namespace Moedi.Cqrs.Handler
{
    public abstract class QueryHandler<TResult>
    {
        internal IQueryRepositoryFactory Uow { get; set; }

        internal ILogger Logger { get; set; }

        protected ILogger UseLogger => Logger;

        protected IQueryRepository<T> GetRepository<T>()
            where T : class, IId
        {
            return Uow.GetRepository<T>();
        }

        public abstract Task<TResult> Query(CancellationToken token);
    }
}