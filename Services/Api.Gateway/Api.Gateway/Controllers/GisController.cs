using Core.Service.Host.Convention.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.Gateway.Controllers
{
    public class GisController : ApiControllerBase
    {
        public GisController(ILogger<GisController> logger, 
            IOptions<ServiceSettings> settings) 
            : base(logger, settings)
        {
            
        }

        /// <summary>
        /// Фильтр по сущностям. 
        /// </summary>
        /// <remarks>
        /// Пример модели запроса делает следующее:
        /// Выбирает все сущности у которых zipCode равен 48601,
        /// сортирует в обратном порядке по firstName, затем по phone
        /// и возвращает первую страницу из пяти сущностей.
        /// Чтобы обратить порядок сортировки нужно стереть "desc" из свойства фильтра sortBy.
        ///
        ///     POST /filter
        ///     {
        ///         "pageNumber": 1,
        ///         "pageSize": 5,
        ///         "sortBy": "firstName, phone desc",
        ///         "firstName": null,
        ///         "city": null,
        ///         "zipCode": "48601",
        ///         "phone": null
        ///     }
        ///
        /// </remarks>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns>Результат работы фильтра</returns>
        //[HttpPost]
        //public async Task<IActionResult> Filter(SearchGisFilterPrj filter, CancellationToken token)
        //{
        //    var result = await _GisProjection.SearchGis(filter, CrossContext(token));
        //    return Ok(result);
        //}
    }
}
