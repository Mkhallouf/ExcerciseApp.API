using AutoMapper;
using ExcerciseApp.API.Domain.Models;
using ExcerciseApp.API.Resources;
using ExcerciseApp.API.Resources.Requests;
using ExcerciseApp.API.Resources.Responces;

namespace ExcerciseApp.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Workout, WorkoutResource>();
            CreateMap<WorkoutResource, Workout>();
            CreateMap<CreateWorkoutResource, Workout>();
        }
    }
}
