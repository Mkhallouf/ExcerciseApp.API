using ExcerciseApp.API.IRepositories;
using ExcerciseApp.API.Models;
using ExcerciseApp.API.Models.Responses;
using ExcerciseApp.API.Models.Responses.Excercise;
using System;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Services
{
    public class ExcerciseService : IExcerciseService
    {
        private readonly IGenericRepository<BaseExcercise> _excerciseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ExcerciseService(IGenericRepository<BaseExcercise> excerciseRepository, IUnitOfWork unitOfWork)
        {
            _excerciseRepository = excerciseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<APIResponse<CreateExcerciseResponse>> CreateAsync(BaseExcercise excercise)
        {
            try
            {
                await _excerciseRepository.InsertAsync(excercise);
                await _unitOfWork.CompleteAsync();

                return new APIResponse<CreateExcerciseResponse>
                {
                    Result = new CreateExcerciseResponse
                    {
                        Id = excercise.WorkoutId,
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Something happen when saving workout: {ex.Message}");
            }
        }

        public async Task<APIResponse<RetrieveExcerciseResponse>> RetrieveAsync(int excerciseId)
        {
            try
            {
                var existingExcercise = await _excerciseRepository.Get((entity) => entity.Id == excerciseId);
                
                if (existingExcercise == null)
                {
                    throw new Exception("Excercise not found");
                }

                return new APIResponse<RetrieveExcerciseResponse>
                {
                    Result = new RetrieveExcerciseResponse
                    {
                        Excercise = existingExcercise,
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Something happen when retrieving an excercise: {ex.Message}");
            }
        }

        public async Task<APIResponse<UpdateExcerciseResponse>> UpdateAsync(int excerciseId, BaseExcercise excercise)
        {
            try
            {
                var existingExcercise = await _excerciseRepository.Get((entity) => entity.Id == excerciseId);

                if (existingExcercise == null)
                {
                    throw new Exception("Excercise not found");
                }

                _excerciseRepository.Update(excercise);
                await _unitOfWork.CompleteAsync();

                return new APIResponse<UpdateExcerciseResponse>
                {
                    Result = new UpdateExcerciseResponse
                    {
                        Excercise = excercise,
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Something happen when updating a excercise: {ex.Message}");
            }
        }

        public async Task<APIResponse<EmptyResponse>> DeleteAsync(int excerciseId)
        {
            var existingExcercise = await _excerciseRepository.Get((entity) => entity.Id == excerciseId);

            if (existingExcercise == null)
            {
                throw new Exception("Excercise not found");
            }

            try
            {
                await _excerciseRepository.DeleteAsync(excerciseId);
                await _unitOfWork.CompleteAsync();

                return new APIResponse<EmptyResponse>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Something happen when deleting excercise: {ex.Message}");
            }
        }
    }
}
