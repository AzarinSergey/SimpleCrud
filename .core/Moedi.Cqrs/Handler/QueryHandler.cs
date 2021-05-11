using Microsoft.Extensions.Logging;
using Moedi.Data.Core.Access;
using Moedi.Data.Core.Entity;
using System.Threading;
using System.Threading.Tasks;

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