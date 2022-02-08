using ExcerciseApp.API.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Domain.Repositories
{
    public interface IWorkoutRepository
    {
        Task<IEnumerable<Workout>> ListAsync();
        Task CreateAsync(Workout workout);
        void Update(Workout workout);
        Task <Workout> FindByIdAsync(int id);
        void Delete(Workout workout);
    }
}
