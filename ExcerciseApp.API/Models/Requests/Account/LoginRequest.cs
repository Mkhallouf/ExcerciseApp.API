using System.ComponentModel.DataAnnotations;

namespace ExcerciseApp.API.Models.Requests.Account
{
    public class LoginRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, ErrorMessage = "Your Password is limited From {2} to {1} characters", MinimumLength = 1)]
        public string Password { get; set; }
    }
}
