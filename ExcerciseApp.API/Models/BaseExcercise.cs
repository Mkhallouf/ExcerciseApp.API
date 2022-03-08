using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcerciseApp.API.Models
{

    [Table("BaseExcercise")]
    public abstract class BaseExcercise
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(Workout))]
        public int WorkoutId { get; set; }

        public Workout Workout { get; set; }
    }
}
