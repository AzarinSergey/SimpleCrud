using AutoMapper;
using Core.Service.Host;
using Microsoft.Extensions.Logging;
using Moedi.Cqrs;
using System;
using System.Threading;
using System.Threading.Tasks;
using Moedi.Data.Core.Access;

namespace Gis.Implementation.Service
{
    public partial class GisService : StatelessService
    {
        private readonly IProcessorFactory _processor;
        private readonly IMapper _mapper;
        private readonly IUowFactory _uowFactory;
        private readonly ILogger<GisService> _logger;

        public GisService(IProcessorFactory processor, IMapper mapper, IUowFactory uowFactory, ILogger<GisService> logger)
        {
            _processor = processor;
            _mapper = mapper;
            _uowFactory = uowFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //TODO: listen service queue here:
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
