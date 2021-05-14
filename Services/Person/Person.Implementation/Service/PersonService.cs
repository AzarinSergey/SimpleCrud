using AutoMapper;
using Core.Service.Host;
using Microsoft.Extensions.Logging;
using Moedi.Cqrs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Person.Implementation.Service
{
    public partial class PersonService : StatelessService
    {
        private readonly IProcessorFactory _processor;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonService> _logger;

        public PersonService(IProcessorFactory processor, IMapper mapper, ILogger<PersonService> logger)
        {
            _processor = processor;
            _mapper = mapper;
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
