using System;

namespace ExcerciseApp.API.Models.Requests.Workout
{
    public class UpdateWorkoutRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
    }
}
