using System.Threading.Tasks;

namespace ExcerciseApp.API.IRepositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
