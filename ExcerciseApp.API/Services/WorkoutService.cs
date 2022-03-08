using AutoMapper;
using ExcerciseApp.API.IRepositories;
using ExcerciseApp.API.Models;
using ExcerciseApp.API.Models.Requests.Workout;
using ExcerciseApp.API.Models.Responses;
using ExcerciseApp.API.Models.Responses.Workout;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IGenericRepository<Workout> _workoutRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WorkoutService(IGenericRepository<Workout> workoutRepository, IUnitOfWork unitOfWork)
        {
            _workoutRepository = workoutRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<APIResponse<CreateWorkoutResponse>> CreateAsync(Workout workout)
        {
            try
            {
                await _workoutRepository.InsertAsync(workout);
                await _unitOfWork.CompleteAsync();

                return new APIResponse<CreateWorkoutResponse>
                {
                    Result = new CreateWorkoutResponse
                    {
                        Id = workout.WorkoutId,
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Something happen when saving workout: {ex.Message}");
            }
        }

        public async Task<APIResponse<EmptyResponse>> DeleteAsync(int id)
        {
            var existingWorkout = await _workoutRepository.Get((entity) => entity.WorkoutId == id);

            if (existingWorkout == null)
            {
                throw new Exception("Workout not found");
            }

            try
            {
                await _workoutRepository.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();

                return new APIResponse<EmptyResponse>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Something happen when deleting workout: {ex.Message}");
            }
        }

        public async Task<APIResponse<IEnumerable<Workout>>> ListAsync()
        {

            try
            {
                var workouts = await _workoutRepository.GetAllAsync();

                return new APIResponse<IEnumerable<Workout>>
                {
                    Result = workouts
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Something happen when fetching all workouts: {ex.Message}");
            }
        }

        public async Task<APIResponse<RetrieveWorkoutResponse>> RetrieveAsync(int id)
        {
            try
            {
                var existingWorkout = await _workoutRepository.Get((entity) => entity.WorkoutId == id);
                if (existingWorkout == null)
                {
                    throw new Exception("Workout not found");
                }

                return new APIResponse<RetrieveWorkoutResponse>
                {
                    Result = new RetrieveWorkoutResponse
                    {
                        Workout = existingWorkout,
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Something happen when retrieving a workout: {ex.Message}");
            }
        }

        public async Task<APIResponse<UpdateWorkoutResponse>> UpdateAsync(int id, Workout workout)
        {
            try
            {
                var existingWorkout = await _workoutRepository.Get((entity) => entity.WorkoutId == id);

                if (existingWorkout == null)
                {
                    throw new Exception("Workout not found");
                }

                _workoutRepository.Update(workout);
                await _unitOfWork.CompleteAsync();

                return new APIResponse<UpdateWorkoutResponse>
                {
                    Result = new UpdateWorkoutResponse
                    {
                        
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Something happen when updating a workout: {ex.Message}");
            }
        }
    }
}

