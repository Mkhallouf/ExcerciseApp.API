using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExcerciseApp.API.Models
{
    public class Workout
    {
        [Key]
        public int WorkoutId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? TimeStart { get; set; } = null;
        public DateTime? TimeEnd { get; set; } = null;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool isCompleted { get; set; } = false;
        public virtual IList<BaseExcercise> Excercises { get; set; } = new List<BaseExcercise>();
    }
}
