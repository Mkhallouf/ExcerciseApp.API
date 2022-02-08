using System;

namespace ExcerciseApp.API.Domain.Models.Requests
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
