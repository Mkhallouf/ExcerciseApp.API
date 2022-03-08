using AutoMapper;
using ExcerciseApp.API.Models;
using ExcerciseApp.API.Models.Requests.Account;
using ExcerciseApp.API.Models.Requests.Excercise;
using ExcerciseApp.API.Models.Requests.Workout;

namespace ExcerciseApp.API.Configuration
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<CreateWorkoutRequest, Workout>().ReverseMap();
            CreateMap<UpdateWorkoutRequest, Workout>().ReverseMap();

            CreateMap<CreateExcerciseRequest, CardioExcercise>().ReverseMap();
            CreateMap<CreateExcerciseRequest, WeightLiftingExcercise>().ReverseMap();

            CreateMap<User, LoginRequest>().ReverseMap();
            CreateMap<User, RegisterRequest>().ReverseMap();
        }
    }
}
