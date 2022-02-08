using AutoMapper;
using ExcerciseApp.API.Domain.Models;
using ExcerciseApp.API.Domain.Models.Requests;
using ExcerciseApp.API.Extensions;
using ExcerciseApp.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Controllers
{
    [ApiController]
    [Route("workout")]
    public class WorkoutOperationController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly IMapper _mapper;

        public WorkoutOperationController(IWorkoutService workoutService, IMapper mapper)
        {
            _workoutService = workoutService;
            _mapper = mapper;
        }

        [HttpPost("/start")]
        public async Task<IActionResult> StartAsync([FromBody] WorkoutRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState.GetErrorMessages());
            }

            try
            {
                var workout = _mapper.Map<WorkoutRequest, Workout>(request);
                workout.TimeStart = DateTime.Now;

                await _workoutService.UpdateAsync(workout.WorkoutId, workout);

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
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }

        [HttpPost("/complete")]
        public async Task<IActionResult> CompleteAsync([FromBody] WorkoutRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState.GetErrorMessages());
            }

            try
            {
                var workout = _mapper.Map<WorkoutRequest, Workout>(request);
                workout.TimeEnd = DateTime.Now;
                workout.isCompleted = true;

                await _workoutService.UpdateAsync(workout.WorkoutId, workout);

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
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }
    }
}
