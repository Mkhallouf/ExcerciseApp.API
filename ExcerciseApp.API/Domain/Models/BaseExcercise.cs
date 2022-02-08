using System.ComponentModel.DataAnnotations.Schema;

namespace ExcerciseApp.API.Domain.Models
{

    [Table("BaseExcercise")]
    public class BaseExcercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("WorkoutId")]
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
    }
}
