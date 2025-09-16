using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayWise.Application.DTOs;
using PayWise.Application.Interfaces;
using PayWise.Core.Entities;
using PayWise.Core.Interfaces;

namespace PayWise.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterationDTO  registerationDTO)
        {
            var result = await _userService.RegisterUserAsync(registerationDTO);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            if (!result.Success || result.Data == null)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            if (!result.Success || result.Data == null)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromQuery] string email, [FromQuery] string password)
        {
            var result = await _userService.AuthenticateAsync(email, password);
            if (!result.Success)
            {
                return Unauthorized(result.ErrorMessage);
            }

            return Ok(new { Token = result.Data });
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> Update(int userId, UserUpdateDTO updateDTO)
        {
            var result = await _userService.UpdateUserAsync(userId, updateDTO);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(int userId, string newPassword)
        {
            var result = await _userService.ChangePasswordAsync(userId, newPassword);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}
