using System.Threading;
using System.Threading.Tasks;

namespace Moedi.Cqrs.Handler
{
    public abstract class QueryHandler<TResult>
    {
        public abstract Task<TResult> Query(CancellationToken token);
    }
}