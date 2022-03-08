using ExcerciseApp.API.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ExcerciseApp.API.Models.Requests.Excercise
{
    public class CreateExcerciseRequest
    {
        [Required]
        public ExcerciseType Type { get; set; }

        [IgnoreDataMember]
        public int WorkoutId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public double Distance { get; set; }
        public int Reps { get; set; }
        public double Wieght { get; set; }
        public int Sets { get; set; }
    }
}