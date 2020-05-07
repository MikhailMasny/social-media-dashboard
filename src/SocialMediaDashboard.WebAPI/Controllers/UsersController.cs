using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaDashboard.Common.Interfaces;
using SocialMediaDashboard.WebAPI.ViewModels;
using System;
using System.Threading.Tasks;

namespace SocialMediaDashboard.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationViewModel model)
        {
            ResponseViewModel responseViewModel;

            if (!ModelState.IsValid)
            {
                responseViewModel = new ResponseViewModel
                {
                    IsSuccessful = false,
                    Message = "Check the correctness of the entered data."
                };

                return BadRequest(responseViewModel);
            }

            var (result, message, user, token) = await _userService.Registration(model.Email, model.Password, model.Name);

            responseViewModel = new ResponseViewModel
            {
                IsSuccessful = result,
                Message = message,
                User = user,
                Token = token
            };

            if (!result)
            {
                return BadRequest(responseViewModel);
            }

            return Ok(responseViewModel);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            ResponseViewModel responseViewModel;

            if (!ModelState.IsValid)
            {
                responseViewModel = new ResponseViewModel
                {
                    IsSuccessful = false,
                    Message = "Email or password is incorrect."
                };

                return BadRequest(responseViewModel);
            }

            var (result, message, user, token) = await _userService.Authenticate(model.Email, model.Password);

            responseViewModel = new ResponseViewModel
            {
                IsSuccessful = result,
                Message = message,
                User = user,
                Token = token
            };

            if (!result)
            {
                return BadRequest(responseViewModel);
            }

            return Ok(responseViewModel);
        }

        // UNDONE: ApiRoute
        [HttpPost("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileViewModel model)
        {
            ResponseViewModel responseViewModel;

            if (!ModelState.IsValid)
            {
                responseViewModel = new ResponseViewModel
                {
                    IsSuccessful = false,
                    Message = "Check the correctness of the entered data."
                };

                return BadRequest(responseViewModel);
            }

            var (result, message, user) = await _userService.UpdateProfile(model.Email, model.Name, model.Avatar);

            responseViewModel = new ResponseViewModel
            {
                IsSuccessful = result,
                Message = message,
                User = user
            };

            if (!result)
            {
                return BadRequest(responseViewModel);
            }

            return Ok(responseViewModel);
        }
    }
}
