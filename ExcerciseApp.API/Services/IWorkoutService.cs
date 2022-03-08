using ExcerciseApp.API.Models;
using ExcerciseApp.API.Models.Responses;
using ExcerciseApp.API.Models.Responses.Workout;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Services
{
    public interface IWorkoutService
    {
        Task<APIResponse<CreateWorkoutResponse>> CreateAsync(Workout workout);
        Task<APIResponse<EmptyResponse>> DeleteAsync(int id);
        Task<APIResponse<UpdateWorkoutResponse>> UpdateAsync(int id, Workout workout);
        Task<APIResponse<RetrieveWorkoutResponse>> RetrieveAsync(int id);
        Task<APIResponse<IEnumerable<Workout>>> ListAsync();
    }
}
