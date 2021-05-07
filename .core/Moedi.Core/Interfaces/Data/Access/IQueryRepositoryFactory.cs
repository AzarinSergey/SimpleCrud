using Moedi.Core.Interfaces.Data.Entity;

namespace Moedi.Core.Interfaces.Data.Access
{
    public interface IQueryRepositoryFactory
    {
        IQueryRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IId;
    }
}