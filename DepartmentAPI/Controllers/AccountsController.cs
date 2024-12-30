using DepartmentBL.DTOs.UserDTO;
using DepartmentBL.Services.Abstractions;
using DepartmentCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        readonly IAuthService _service;
        public AccountsController(IAuthService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                IEnumerable<UserDTO> users = await _service.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetOneUser(string userName)
        {
            try
            {
                UserDTO user = await _service.GetOneUserAsync(userName);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return BadRequest("Invalid email confirmation request.");
            }

            try
            {
                var result = await _service.ConfirmEmailAsync(userId, token);
                if (result)
                {
                    return Ok("Email confirmed successfully.");
                }

                return BadRequest("Email confirmation failed.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("You have to fill in all inputs.");
            }
            try
            {
                await _service.RegisterAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("You have to fill in all inputs.");
            }
            try
            {
                await _service.LoginAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet]
        //[Route("logout")]
        //public async Task Logout()
        //{
        //    await _service.LogoutAsync();
        //}

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email is required.");
            }

            try
            {
                await _service.ForgotPasswordAsync(email);
                return Ok("Password reset link has been sent to your email.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input.");
            }

            try
            {
                await _service.ResetPasswordAsync(model.Email, model.Token, model.NewPassword);
                return Ok("Password has been reset successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input.");
            }
            try
            {
                await _service.ChangePasswordAsync(model.Email, model.Password, model.NewPassword);
                return Ok("Password has been reset successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
