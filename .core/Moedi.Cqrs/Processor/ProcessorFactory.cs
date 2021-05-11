using Microsoft.Extensions.Logging;
using Moedi.Core.Interfaces.Data.Access;
using Moedi.Cqrs.Handler;
using Moedi.Cqrs.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedi.Cqrs.Processor
{
    public class ProcessorFactory : IProcessorFactory
    {
        private readonly IUowFactory _uowFactory;
        private readonly ILoggerFactory _loggerFactory;

        public ProcessorFactory(IUowFactory uowFactory, ILoggerFactory loggerFactory)
        {
            _uowFactory = uowFactory;
            _loggerFactory = loggerFactory;
        }

        public CommandProcessorBuilder<TDomainMessage> ForCommand<TDomainMessage>(IntegrationMessage command)
            where TDomainMessage : DomainMessage
        {
            _loggerFactory.CreateLogger($"ProcessorFactory[{command.CrossContext.CorrelationUuid}]")
                .LogInformation($"Prepare command processing for '{nameof(command)}'");

            return new CommandProcessorBuilder<TDomainMessage>(command.CrossContext, _uowFactory, _loggerFactory);
        }

        public async Task<T> ForQuery<T>(CrossContext ctx, CancellationToken token, QueryHandler<T> handler)
        {
            var logger = _loggerFactory.CreateLogger($"QueryProcessor[{nameof(handler)}][{ctx.CorrelationUuid}]");
            logger.LogInformation($"Started at {DateTime.Now}");

            try
            {
                using (var uow = _uowFactory.CreateUnitOfWork(null, token))
                {
                    handler.Uow = uow;
                    handler.Logger = logger;
                    var result = await handler.Query(token);

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.LogError("{0}\n{1}", e.Message, e.StackTrace);
                throw;
            }
            finally
            {
                logger.LogInformation($"Done at: {DateTime.Now}");
            }
        }
    }
}