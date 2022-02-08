using System;
using System.ComponentModel.DataAnnotations;

namespace ExcerciseApp.API.Domain.Models.Requests
{
    public class CreateWorkoutRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
