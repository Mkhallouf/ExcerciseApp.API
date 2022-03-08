using System.ComponentModel.DataAnnotations.Schema;

namespace ExcerciseApp.API.Models
{
    [Table("WeightLiftingExcercise")]
    public class WeightLiftingExcercise : BaseExcercise
    {
        public int Reps { get; set; }
        public double Wieght { get; set; }
        public int Sets { get; set; }
    }
}
