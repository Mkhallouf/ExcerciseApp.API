using AutoMapper;
using ExcerciseApp.API.Extensions;
using ExcerciseApp.API.Models;
using ExcerciseApp.API.Models.Requests.Excercise;
using ExcerciseApp.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Controllers
{
    [ApiController]
    public class ExcerciseController : ControllerBase
    {
        private readonly IExcerciseService _excerciseService;
        private readonly IMapper _mapper;
        private readonly ILogger<ExcerciseController> _logger;

        public ExcerciseController(IExcerciseService workoutService, IMapper mapper, ILogger<ExcerciseController> logger)
        {
            _excerciseService = workoutService;
            _mapper = mapper;
            _logger = logger;
        }

        [Route("/workout/{workoutId}/excercise")]
        [HttpPost]
        public async Task<IActionResult> PostAsync(int workoutId, [FromBody] CreateExcerciseRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState.GetErrorMessages());
            }

            try
            {
                BaseExcercise excercise;

                if (request.Type == ExcerciseType.WeightLifting)
                {
                    excercise = _mapper.Map<WeightLiftingExcercise>(request);
                    excercise.WorkoutId = workoutId;

                }
                else
                {
                    excercise = _mapper.Map<CardioExcercise>(request);
                    excercise.WorkoutId = workoutId;
                }

                await _excerciseService.CreateAsync(excercise);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(PostAsync)}");
                return new BadRequestResult();
            }
        }

        [Route("/workout/{workoutId}/excercise/{excerciseId}")]
        [HttpGet]
        public async Task<IActionResult> GetAsync(int workoutId, int excerciseId, [FromServices] IWorkoutService workoutService)
        {
            try
            {
                await workoutService.RetrieveAsync(workoutId);
                var response = await _excerciseService.RetrieveAsync(excerciseId);

                return new OkObjectResult(response.Result);
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("not found"))
                {
                    return new NotFoundResult();
                }
                else
                {
                    _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetAsync)}");
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }

        [Route("/workout/{workoutId}/excercise/{excerciseId}")]
        [HttpPut]
        public async Task<IActionResult> PutAsync(int workoutId, int excerciseId, UpdateExcerciseRequest request, [FromServices] IWorkoutService workoutService)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState.GetErrorMessages());
            }

            try
            {
                await workoutService.RetrieveAsync(workoutId);

                BaseExcercise excercise;

                if (request.Type == ExcerciseType.WeightLifting)
                {
                    excercise = _mapper.Map<WeightLiftingExcercise>(request);
                    excercise.WorkoutId = workoutId;

                }
                else
                {
                    excercise = _mapper.Map<CardioExcercise>(request);
                    excercise.WorkoutId = workoutId;
                }

                var response = await _excerciseService.UpdateAsync(excerciseId, excercise);

                return new OkObjectResult(response.Result);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                {
                    return new NotFoundResult();
                }
                else
                {
                    _logger.LogError(ex, $"Something Went Wrong in the {nameof(PutAsync)}");
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }

        [Route("/workout/{workoutId}/excercise/{excerciseId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int workoutId, int excerciseId, [FromServices] IWorkoutService workoutService)
        {
            try
            {
                await workoutService.RetrieveAsync(workoutId);
                await _excerciseService.DeleteAsync(excerciseId);

                return new OkResult();
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("not found"))
                {
                    return new NotFoundResult();
                }
                else
                {
                    _logger.LogError(ex, $"Something Went Wrong in the {nameof(DeleteAsync)}");
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }
    }
}
