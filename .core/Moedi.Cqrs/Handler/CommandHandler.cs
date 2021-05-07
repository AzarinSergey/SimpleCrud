using System.Threading;
using System.Threading.Tasks;
using Moedi.Cqrs.Messages;

namespace Moedi.Cqrs.Handler
{
    public abstract class CommandHandler<T>
        where T : DomainMessage
    {
        public abstract Task Execute(T command, CancellationToken token);
    }
}
