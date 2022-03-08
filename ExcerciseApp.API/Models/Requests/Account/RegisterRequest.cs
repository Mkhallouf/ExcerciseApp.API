using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExcerciseApp.API.Models.Requests.Account
{
    public class RegisterRequest : LoginRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
