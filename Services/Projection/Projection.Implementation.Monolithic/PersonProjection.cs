using AutoMapper;
using Cmn.Models;
using Moedi.Cqrs;
using Moedi.Cqrs.Messages;
using Projection.Contract;
using Projection.Contract.Models;
using Projection.Domain.Model;
using Projection.Domain.Query;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Projection.Implementation.Monolithic
{
    public class PersonProjection : IPersonProjection
    {
        private readonly IProcessorFactory _processor;
        private readonly IMapper _mapper;

        public PersonProjection(IProcessorFactory processor, IMapper mapper)
        {
            _processor = processor;
            _mapper = mapper;
        }

        public async Task<PagedResult<SearchPersonResultPrj>> SearchPerson(SearchPersonFilterPrj model, CrossContext ctx, CancellationToken token)
        {
            var filter = _mapper.Map<SearchPersonFilter>(model);
            var query = new SearchPersonFilterQuery(filter);
            var result = await _processor.Query(ctx, token, query);

            return new PagedResult<SearchPersonResultPrj>(result.PageSize, result.PageNumber, result.TotalCount, 
                _mapper.Map<List<SearchPersonResultPrj>>(result.Items));
        }
    }
}
