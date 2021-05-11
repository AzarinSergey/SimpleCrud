using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedi.Core.Interfaces.Data.Access;
using Moedi.Core.Interfaces.Data.Entity;
using Moedi.Cqrs.Messages;

namespace Moedi.Cqrs.Handler
{
    public abstract class CommandHandler<TCommand>
        where TCommand : DomainMessage
    {
        internal ICommandRepositoryFactory Uow { get; set; }

        internal ILogger Logger { get; set; }

        protected ILogger UseLogger => Logger;

        protected CommandHandler()
        {
            EventList = new List<DomainEvent>();
        }

        protected ICommandRepository<T> GetRepository<T>()
            where T : class, IId
        {
            return Uow.GetRepository<T>();
        }

        internal List<DomainEvent> EventList;

        protected void Raise(DomainEvent e)
            => EventList.Add(e);

        protected void Raise(IEnumerable<DomainEvent> e)
            => EventList.AddRange(e);

        public abstract Task Execute(TCommand command, CancellationToken token);
    }
}
