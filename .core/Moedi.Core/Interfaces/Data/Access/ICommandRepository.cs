using Moedi.Core.Interfaces.Data.Entity;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Moedi.Core.Interfaces.Data.Access
{
    public interface ICommandRepository<TEntity> : IQueryRepository<TEntity>
         where TEntity : class, IId
    {
        Task<int> CreateOrUpdateAsync(TEntity entity);

        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> condition);
    }
}
