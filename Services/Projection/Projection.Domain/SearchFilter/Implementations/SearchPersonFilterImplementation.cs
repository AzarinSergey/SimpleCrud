using System.Collections.Generic;
using System.Linq;
using Moedi.Data.Core.Access;
using Projection.Domain.Model;
using Projection.Domain.SearchFilter.Criteria;
using Projection.Domain.SearchFilter.Extensions;

namespace Projection.Domain.SearchFilter.Implementations
{
    public static class SearchPersonFilterImplementation
    {
        public static FilterQuery<Person.Model.Entity.Person> GetQuery(this SearchPersonFilter filter, IQueryRepositoryFactory factory)
        {
            var personQuery = factory.GetRepository<Person.Model.Entity.Person>().Query();
            var criteria = GetCriterias(filter).Where(x => x != null);

            foreach (var c in criteria)
            {
                personQuery = from query in personQuery
                              join criteriaQuery in c.GetQuery(factory) 
                                  on query.Id equals criteriaQuery 
                              select query;
            }

            //это не очень хорошо
            personQuery = personQuery.Distinct();

            return new FilterQuery<Person.Model.Entity.Person> 
            { 
                Query = personQuery
                    .GenericSort(filter.SortBy, persons => persons.OrderBy(x => x.Id))
                    .Skip(filter.PageSize * (filter.PageNumber - 1))
                    .Take(filter.PageSize),

                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalCount = personQuery.Count()
            };
        }

        private static IEnumerable<ISearchCriteria> GetCriterias(SearchPersonFilter filter)
        {
            yield return filter.City.MapString(city => new PersonExpressionCriteria(p => p.City.Contains(city)));
            yield return filter.ZipCode.MapString(zipCode => new PersonExpressionCriteria(p => p.ZipCode.Contains(zipCode)));
            yield return filter.FirstName.MapString(fName => new PersonExpressionCriteria(p => p.FirstName.Contains(fName)));
            yield return filter.Phone.MapString(phone => new PersonExpressionCriteria(p => p.PhoneNumber.Contains(phone)));
        }

        public static void NormalizePagination(this SearchPersonFilter filter)
        {
            if (filter.PageSize <= 0)
                filter.PageSize = 5;

            if (filter.PageNumber <= 0)
                filter.PageNumber = 1;
        }
    }
}