using System;

namespace Moedi.Data.Core.Access
{
    public interface IUow : IDisposable,
        ICommandRepositoryFactory,
        IQueryRepositoryFactory
    {
        void Commit();
        void Rollback();
    }
}