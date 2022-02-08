using ExcerciseApp.API.Domain.Models;

namespace ExcerciseApp.API.Communication.Responces
{
    public class WorkoutResponse : BaseResponse
    {
        public Workout Workout { get; set; }

        public WorkoutResponse(bool success, string message, Workout workout) : base(success, message)
        {
            Workout = workout;
        }

        public WorkoutResponse(Workout workout) : this(true, string.Empty, workout)
        {
        }

        public WorkoutResponse(string message) : this(false, message, null)
        {
        }
    }
}
