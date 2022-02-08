using ExcerciseApp.API.Communication.Responces;
using ExcerciseApp.API.Domain.Models;
using ExcerciseApp.API.Domain.Repositories;
using ExcerciseApp.API.Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WorkoutService(IWorkoutRepository workoutRepository, IUnitOfWork unitOfWork)
        {
            _workoutRepository = workoutRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkoutResponse> CreateAsync(Workout workout)
        {
            try
            {
                await _workoutRepository.CreateAsync(workout);
                await _unitOfWork.CompleteAsync();

                return new WorkoutResponse(workout);
            }
            catch (Exception ex)
            {
                return new WorkoutResponse($"Something happen when saving workout: {ex.Message}");
            }
        }

        public async Task<WorkoutResponse> DeleteAsync(int id)
        {
            var existingWorkout = await _workoutRepository.FindByIdAsync(id);
            if (existingWorkout == null)
            {
                throw new Exception("Workout not found");
            }

            try
            {
                _workoutRepository.Delete(existingWorkout);
                await _unitOfWork.CompleteAsync();

                return new WorkoutResponse(existingWorkout);
            }
            catch (Exception ex)
            {
                return new WorkoutResponse($"Something happen when deleting workout: {ex.Message}");

            }
        }

        public async Task<IEnumerable<Workout>> ListAsync()
        {
            return await _workoutRepository.ListAsync();
        }

        public async Task<WorkoutResponse> RetrieveAsync(int id)
        {
            try
            {
                var existingWorkout = await _workoutRepository.FindByIdAsync(id);
                if (existingWorkout == null)
                {
                    throw new Exception("Workout not found");
                }

                return new WorkoutResponse(existingWorkout);
            }
            catch (Exception ex)
            {
                return new WorkoutResponse($"Can't retrieve the workout: {ex.Message}");
            }
        }

        public async Task<WorkoutResponse> UpdateAsync(int id, Workout workout)
        {
            var existingWorkout = await _workoutRepository.FindByIdAsync(id);

            if (existingWorkout == null)
            {
                throw new Exception("Workout not found");
            }

            try
            {
                _workoutRepository.Update(workout);
                await _unitOfWork.CompleteAsync();

                return new WorkoutResponse(existingWorkout);
            }
            catch (Exception ex)
            {
                return new WorkoutResponse($"Can't update the workout: {ex.Message}");
            }
        }
    }
}
