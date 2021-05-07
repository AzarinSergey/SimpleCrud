using System;

namespace Moedi.Core.Interfaces.Data.Access
{
    public interface IUow : IDisposable,
        ICommandRepositoryFactory,
        IQueryRepositoryFactory
    {
        void Commit();
        void Rollback();
    }
}