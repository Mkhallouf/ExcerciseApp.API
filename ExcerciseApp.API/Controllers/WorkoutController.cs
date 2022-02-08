using AutoMapper;
using ExcerciseApp.API.Domain.Models;
using ExcerciseApp.API.Domain.Models.Requests;
using ExcerciseApp.API.Extensions;
using ExcerciseApp.API.Resources;
using ExcerciseApp.API.Resources.Requests;
using ExcerciseApp.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Controllers
{
    [ApiController]
    [Route("workout")]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly IMapper _mapper;

        public WorkoutController(IWorkoutService workoutService, IMapper mapper)
        {
            _workoutService = workoutService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var result = await _workoutService.RetrieveAsync(id);
                var response = _mapper.Map<Workout, WorkoutResource>(result.Workout);

                return new OkObjectResult(response);
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

        [HttpGet]
        public async Task<IEnumerable<WorkoutResource>> GetAllAsync()
        {
            var workouts = await _workoutService.ListAsync();
            var response = _mapper.Map<IEnumerable<Workout>, IEnumerable<WorkoutResource>>(workouts);

            return response;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateWorkoutRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState.GetErrorMessages());
            }

            var workout = _mapper.Map<CreateWorkoutRequest, Workout>(request);

            await _workoutService.CreateAsync(workout);

            return Ok();
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
                var workout = _mapper.Map<UpdateWorkoutRequest, Workout>(request);
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
                    return new BadRequestObjectResult(ex.Message);
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _workoutService.DeleteAsync(id);

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
