using Core.Service.Host;
using Core.Service.Host.Convention.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.Gateway.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class ApiControllerBase : AbstractController
    {
        private readonly string _serviceUuid;

        protected ApiControllerBase(ILogger<ApiControllerBase> logger, IOptions<ServiceSettings> settings) : base(logger)
        {
            _serviceUuid = settings.Value.ServiceName;
        }

        protected override string ServiceUuid => _serviceUuid;
    }
}