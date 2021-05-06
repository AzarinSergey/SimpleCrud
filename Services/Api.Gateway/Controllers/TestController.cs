using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleCrud;

namespace Api.Gateway.Controllers
{
    public class TestController : ApiControllerBase
    {
        public TestController(ILogger<TestController> logger) 
            : base(logger)
        {  }

        [HttpGet]
        public IActionResult Get()
        {
            Logger.LogDebug(nameof(Get));
            return Ok("Alive");
        }
    }
}
