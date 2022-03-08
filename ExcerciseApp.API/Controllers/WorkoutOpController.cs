using AutoMapper;
using ExcerciseApp.API.Extensions;
using ExcerciseApp.API.Models;
using ExcerciseApp.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Controllers
{
    [Route("workoutop")]
    [ApiController]
    public class WorkoutOpController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly ILogger<WorkoutOpController> _logger;

        public WorkoutOpController(IWorkoutService workoutService, ILogger<WorkoutOpController> logger)
        {
            _workoutService = workoutService;
            _logger = logger;
        }

        [HttpGet("/start/{id}")]
        public async Task<IActionResult> StartAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState.GetErrorMessages());
            }

            try
            {
                var workout = new Workout()
                {
                    WorkoutId = id,
                    TimeStart = DateTime.Now,
                };

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
                    _logger.LogError(ex, $"Something Went Wrong in the {nameof(StartAsync)}");
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }

        [HttpGet("/complete/{id}")]
        public async Task<IActionResult> CompleteAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState.GetErrorMessages());
            }

            try
            {
                var workout = new Workout()
                {
                    WorkoutId = id,
                    TimeEnd = DateTime.Now,
                    isCompleted = true
                };

                await _workoutService.UpdateAsync(id, workout);

                // TODO : Calculate total burned calories

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
                    _logger.LogError(ex, $"Something Went Wrong in the {nameof(CompleteAsync)}");
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }
    }
}
