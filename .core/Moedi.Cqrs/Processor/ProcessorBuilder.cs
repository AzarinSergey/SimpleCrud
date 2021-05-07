using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedi.Core.Interfaces.Data.Access;
using Moedi.Cqrs.Handler;
using Moedi.Cqrs.Messages;

namespace Moedi.Cqrs.Processor
{
    public class ProcessorBuilder
    {
        private readonly IUowFactory _uowFactory;
        private readonly ILoggerFactory _loggerFactory;

        public ProcessorBuilder(IUowFactory uowFactory, ILoggerFactory loggerFactory)
        {
            _uowFactory = uowFactory;
            _loggerFactory = loggerFactory;
        }

        public CommandProcessorBuilder ForCommand(CrossContext ctx)
        {
            return new CommandProcessorBuilder(ctx, _uowFactory);
        }

        public async Task<T> ForQuery<T>(CrossContext ctx, CancellationToken token, QueryHandler<T> handler)
        {
            var logger = _loggerFactory.CreateLogger($"QueryProcessor[{nameof(handler)}][{ctx.CorrelationUuid}]");
            
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                logger.LogInformation("Started");
                using (var uow = _uowFactory.CreateUnitOfWork(null, token))
                {
                    handler.Uow = uow;
                    handler.Logger = logger;
                    var result = await handler.Query(token);
                    
                    logger.LogInformation($"Done. ExecutionTime: {stopWatch.Elapsed}");

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.LogError("{0}\n{1}", e.Message, e.StackTrace);
                throw;
            }
        }
    }
}