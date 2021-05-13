using System;
using System.Linq;
using System.Linq.Expressions;
using Moedi.Data.Core.Access;

namespace Projection.Domain.SearchFilter.Criteria
{
    public class PersonExpressionCriteria : ISearchCriteria
    {
        private readonly Expression<Func<Person.Model.Entity.Person, bool>> _exp;

        public PersonExpressionCriteria(Expression<Func<Person.Model.Entity.Person, bool>> exp)
        {
            _exp = exp;
        }

        public IQueryable<int> GetQuery(IQueryRepositoryFactory f)
        {
            return f.GetRepository<Person.Model.Entity.Person>().Query().Where(_exp).Select(x => x.Id);
        }
    }
}