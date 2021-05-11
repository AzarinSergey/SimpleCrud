using Microsoft.Extensions.Logging;
using Moedi.Cqrs.Handler;
using Moedi.Cqrs.Messages;
using Moedi.Data.Core.Access;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedi.Cqrs.Processor
{
    internal class DefaultCommandProcessor<TCommand> : ICommandProcessor<TCommand>
        where TCommand : DomainMessage
    {
        private readonly Func<CommandHandler<TCommand>> _handlerBuilder;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IUowFactory _uowfactory;

        internal DefaultCommandProcessor(Func<CommandHandler<TCommand>> handlerBuilder, ILoggerFactory loggerFactory, IUowFactory uowFactory)
        {
            _handlerBuilder = handlerBuilder;
            _loggerFactory = loggerFactory;
            _uowfactory = uowFactory;
        }

        public bool UseTransaction { get; set; }

        public Task Process(TCommand command, CrossContext ctx, CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public Task<DomainEvent[]> ProcessWithEvents(TCommand command, CrossContext ctx, CancellationToken token)
        {
            var handler = _handlerBuilder();
            handler.Uow = _uowfactory.CreateUnitOfWork(null);
            handler.Logger = _loggerFactory.CreateLogger(nameof(handler));
            
            return Task.FromResult(handler.EventList.ToArray());
        }
    }
}
