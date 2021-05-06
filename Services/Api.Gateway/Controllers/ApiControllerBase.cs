using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected readonly ILogger<TestController> Logger;

        protected ApiControllerBase(ILogger<TestController> logger)
        {
            Logger = logger;
        }
    }
}