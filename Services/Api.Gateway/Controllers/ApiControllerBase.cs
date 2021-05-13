using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moedi.Cqrs.Messages;

namespace Api.Gateway.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected readonly ILogger<ApiControllerBase> Logger;

        private CrossContext _ctx;
        protected CrossContext CrossContext => _ctx ??= new CrossContext { CorrelationUuid = Guid.NewGuid() };

        protected ApiControllerBase(ILogger<ApiControllerBase> logger)
        {
            Logger = logger;
        }
    }
}