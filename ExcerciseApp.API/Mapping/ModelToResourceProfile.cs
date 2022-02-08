using AutoMapper;
using ExcerciseApp.API.Domain.Models;
using ExcerciseApp.API.Domain.Models.Requests;
using ExcerciseApp.API.Resources.Requests;

namespace ExcerciseApp.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Workout, WorkoutResource>();
            CreateMap<WorkoutResource, Workout>();
            CreateMap<CreateWorkoutRequest, Workout>();
        }
    }
}
