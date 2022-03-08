using ExcerciseApp.API.Models.Requests.Account;
using ExcerciseApp.API.Models.Responses;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Services
{
    public interface IAuthService
    {
        Task<APIResponse<bool>> ValidateUserAsync(LoginRequest request);
        Task<APIResponse<string>> GenerateTokenAsync();
    }
}
