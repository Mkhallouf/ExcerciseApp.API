using AutoMapper;
using ExcerciseApp.API.Models;
using ExcerciseApp.API.Models.Requests.Account;
using ExcerciseApp.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;
        private readonly IAuthService _authService;

        public AccountController(UserManager<User> userManager, ILogger<AccountController> logger, IMapper mapper, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            _logger.LogInformation($"Registration Attempt for {request.Email}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<User>(request);
                user.UserName = request.Email;

                var result = await _userManager.CreateAsync(user);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    return new BadRequestObjectResult(ModelState);
                }

                var ad = await _userManager.AddPasswordAsync(user, request.Password);
                await _userManager.AddToRolesAsync(user, request.Roles);
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RegisterAsync)}");
                return new BadRequestResult();
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var validationResponse = await _authService.ValidateUserAsync(request);
                if (!validationResponse.Result)
                {
                    return Unauthorized(request);
                }

                var tokenResponse = await _authService.GenerateTokenAsync();

                return Accepted(new { Token = tokenResponse.Result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(LoginAsync)}");
                return new BadRequestResult();
            }
        }
    }
}
