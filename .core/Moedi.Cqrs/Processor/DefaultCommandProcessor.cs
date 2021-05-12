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

        public async Task Process(TCommand command, CrossContext ctx, CancellationToken token)
        {
            var logger = _loggerFactory.CreateLogger($"CommandProcessor[{nameof(command)}][{ctx.CorrelationUuid}]");
            IUow uow = null;
            try
            {
                logger.LogInformation($"Started at {DateTime.Now}");
                var handler = _handlerBuilder();
                var transactionUuid = UseTransaction ? Guid.NewGuid() : (Guid?) null;
                uow = _uowfactory.CreateUnitOfWork(transactionUuid, token);
                handler.Uow = uow;
                handler.Logger = _loggerFactory.CreateLogger(nameof(handler));

                await handler.Execute(command, token);

                await uow.Commit();
            }
            catch (Exception e)
            {
                logger.LogError("{0}\n{1}", e.Message, e.StackTrace);
            }
            finally
            {
                var t = uow?.Rollback();
                if (t != null) 
                    await t;

                uow?.Dispose();

                logger.LogInformation($"Done at {DateTime.Now}");
            }
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
