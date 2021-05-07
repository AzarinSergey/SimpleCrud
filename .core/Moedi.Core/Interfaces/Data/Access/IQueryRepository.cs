using Moedi.Core.Interfaces.Data.Entity;
using System.Linq;

namespace Moedi.Core.Interfaces.Data.Access
{
    public interface IQueryRepository<TEntity>
        where TEntity : class, IId
    {
        IQueryable<TEntity> Query();
    }
}