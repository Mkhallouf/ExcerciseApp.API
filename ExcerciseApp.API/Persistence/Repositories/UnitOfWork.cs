using ExcerciseApp.API.Domain.Repositories;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CompleteAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
