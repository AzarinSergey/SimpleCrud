using System.Threading;
using System.Threading.Tasks;
using Core.Service.Host.Client.DynamicProxy;
using Core.Service.Host.Convention.Configuration;
using Gis.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.Gateway.Controllers
{
    public class GisController : ApiControllerBase
    {
        private readonly ServiceProxy<IGisService> _gisService;

        public GisController(ILogger<GisController> logger, 
            IOptions<ServiceSettings> settings,
            ServiceProxy<IGisService> gisService) 
            : base(logger, settings)
        {
            _gisService = gisService;
        }

        /// <summary>
        /// Получить расстояние между аэропортами
        /// </summary>
        /// <remarks>
        /// Ремарка
        /// </remarks>
        /// <param name="a">Код аэропорта</param>
        /// <param name="b">Код аэропорта</param>
        /// <param name="token">Токен отмены</param>
        /// <returns>Расстояние в милях</returns>
        [HttpGet("/distance/{a}/{b}")]
        public async Task<IActionResult> GetDistance(string a, string b, CancellationToken token)
        {
            var result = await _gisService.Call()
                .GetDistance(new GetDistanceModel
                {
                    AirportCodeA = a,
                    AirportCodeB = b
                }, CrossContext(token));

            return Ok(result);
        }
    }
}
