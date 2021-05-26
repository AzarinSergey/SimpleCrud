using System.Threading;
using System.Threading.Tasks;
using Core.Service.Host.Client.DynamicProxy;
using Core.Service.Host.Convention.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Person.Contract;
using Projection.Contract;
using Projection.Contract.Models;

namespace Api.Gateway.Controllers
{
    public class PersonController : ApiControllerBase
    {
        private readonly ServiceProxy<IPersonService> _personService;
        private readonly IPersonProjection _personProjection;

        public PersonController(ILogger<PersonController> logger, 
            IOptions<ServiceSettings> settings,
            ServiceProxy<IPersonService> personService, 
            IPersonProjection personProjection) 
            : base(logger, settings)
        {
            _personService = personService;
            _personProjection = personProjection;
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
        [HttpPost]
        public async Task<IActionResult> Filter(SearchPersonFilterPrj filter, CancellationToken token)
        {
            var result = await _personProjection.SearchPerson(filter, CrossContext(token));
            return Ok(result);
        }

        /// <summary>
        /// Создать сущность
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns>Идентификатор новой сущности</returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonCommandModel model, CancellationToken token)
        {
            var result = await _personService.Call().CreatePerson(model, CrossContext(token));
            return Ok(result);
        }

        /// <summary>
        /// Обновить сущность
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns>Индикатор успеха</returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> Update(int id, CreatePersonCommandModel model, CancellationToken token)
        {
            var result = await _personService.Call().UpdatePerson(id, model, CrossContext(token));

            return Ok(result);
        }

        /// <summary>
        /// Удвлить сущность
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns>Модель удаленной сущности</returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> Remove(int id, CancellationToken token)
        {
            var result = await _personService.Call().RemovePerson(id, CrossContext(token));

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
