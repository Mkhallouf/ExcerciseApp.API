using ExcerciseApp.API.Models;
using ExcerciseApp.API.Models.Responses;
using ExcerciseApp.API.Models.Responses.Excercise;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Services
{
    public interface IExcerciseService
    {
        Task<APIResponse<CreateExcerciseResponse>> CreateAsync(BaseExcercise excercise);
        Task<APIResponse<EmptyResponse>> DeleteAsync(int id);
        Task<APIResponse<UpdateExcerciseResponse>> UpdateAsync(int id, BaseExcercise excercise);
        Task<APIResponse<RetrieveExcerciseResponse>> RetrieveAsync(int id);
    }
}
