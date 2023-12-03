using SqlPersistence.Abstract;
using SqlPersistence.Abstract.Repositories;
using SqlPersistence.Persistence.Repositories;

namespace SqlPersistence.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;


        public IPermissionRepository Permissions { get; set; }

        public IPermissionTypeRepository PermissionTypes { get; set; }

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Permissions = new PermissionRepository(_context);
            PermissionTypes = new PermissionTypeRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public Task<int> CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
