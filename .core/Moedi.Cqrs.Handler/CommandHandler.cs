using Moedi.Cqrs.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace Moedi.Cqrs.Handler
{
    public abstract class CommandHandler<T>
        where T : DomainMessage
    {
        public abstract Task Execute(T command, CancellationToken token);
    }
}
