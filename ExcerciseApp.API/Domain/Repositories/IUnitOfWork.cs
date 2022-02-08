using System.Threading.Tasks;

namespace ExcerciseApp.API.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
