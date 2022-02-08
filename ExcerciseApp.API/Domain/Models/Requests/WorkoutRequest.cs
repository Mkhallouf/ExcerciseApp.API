using System.ComponentModel.DataAnnotations;

namespace ExcerciseApp.API.Domain.Models.Requests
{
    public class WorkoutRequest
    {
        [Required]
         public int WorkoutId { get; set; }
    }
}
