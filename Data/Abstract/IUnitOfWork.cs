//using SqlPersistence.Abstract.Repositories;

using SqlPersistence.Abstract.Repositories;

namespace SqlPersistence.Abstract
{
    public interface IUnitOfWork : IDisposable
    {

        public IPermissionRepository Permissions { get; }
        public IPermissionTypeRepository PermissionTypes { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}
