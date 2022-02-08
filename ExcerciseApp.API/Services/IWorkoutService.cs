using ExcerciseApp.API.Communication.Responces;
using ExcerciseApp.API.Domain.Models;
using ExcerciseApp.API.Resources.Responces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Services
{
    public interface IWorkoutService
    {
        Task<WorkoutResponse> CreateAsync(Workout workout);
        Task<WorkoutResponse> DeleteAsync(int id);
        Task<WorkoutResponse> UpdateAsync(int id, Workout workout);
        Task<WorkoutResponse> RetrieveAsync(int id);
        Task<IEnumerable<Workout>> ListAsync();
    }
}
