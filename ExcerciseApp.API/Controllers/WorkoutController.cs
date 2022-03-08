using AutoMapper;
using ExcerciseApp.API.Extensions;
using ExcerciseApp.API.Models;
using ExcerciseApp.API.Models.Requests.Workout;
using ExcerciseApp.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Controllers
{
    [ApiController]
    [Route("workout")]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly IMapper _mapper;
        private readonly ILogger<WorkoutController> _logger;

        public WorkoutController(IWorkoutService workoutService, IMapper mapper, ILogger<WorkoutController> logger)
        {
            _workoutService = workoutService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var response = await _workoutService.RetrieveAsync(id);

                return new OkObjectResult(response.Result);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Workout not found"))
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

        [HttpGet]
        public async Task<IEnumerable<Workout>> GetAllAsync()
        {
            try
            {
                var response = await _workoutService.ListAsync();
             return response.Result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetAllAsync)}");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateWorkoutRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState.GetErrorMessages());
            }

            try
            {
                var workout = _mapper.Map<Workout>(request);
                await _workoutService.CreateAsync(workout);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(PostAsync)}");
                return new BadRequestResult();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateWorkoutRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState.GetErrorMessages());
            }

            try
            {
                var workout = _mapper.Map<Workout>(request);
                await _workoutService.UpdateAsync(id, workout);

                return new OkResult();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Workout not found"))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                _ = await _workoutService.DeleteAsync(id);

                return new OkResult();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Workout not found"))
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
