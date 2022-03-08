using ExcerciseApp.API.IRepositories;
using ExcerciseApp.API.Models;
using System;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _appDbContext;

        public UnitOfWork(DatabaseContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CompleteAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
        
        private bool disposed = false;
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _appDbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
