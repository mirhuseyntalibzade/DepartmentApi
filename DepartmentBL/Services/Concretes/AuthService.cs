using AutoMapper;
using DepartmentBL.DTOs.UserDTO;
using DepartmentBL.Services.Abstractions;
using DepartmentCore.Models;
using DepartmentDAL.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DepartmentBL.Services.Concretes
{
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly IEmailService _emailService;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public AuthService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDBContext context, IEmailService emailService, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User not found with this email.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var resetLink = $"{baseUrl}api/accounts/reset-password?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";

            var emailBody = $"<p>Hello,</p><p>Click the link below to reset your password:</p><p><a href='{resetLink}'>Reset Password</a></p>";

            await _emailService.SendEmailAsync(email, "Reset Your Password", emailBody);
            
            
            //Manual olaraq tokeni alıb swagger'da çalışdırmaq üçün:
            //To get token manually from console and make it work manually by typing it to the swagger.
            Console.WriteLine(token);
        }

        public async Task ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to reset password.");
            }
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Invalid user.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                throw new Exception("Email confirmation failed.");
            }
            return true;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            List<AppUser> users =  await _userManager.Users.ToListAsync();

            IEnumerable<UserDTO> userDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);

            return userDTOs;
        }

        public async Task<UserDTO> GetOneUserAsync(string userName)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u=>u.NormalizedUserName == userName.ToUpper());
            if (user is null)
            {
                throw new Exception("User cannot be found.");
            }

            UserDTO userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task<bool> LoginAsync(LoginUserDTO userDTO)
        {
            AppUser? user = await _userManager.FindByNameAsync(userDTO.EmailOrUserName)
                       ?? await _userManager.FindByEmailAsync(userDTO.EmailOrUserName);

            if (user == null)
            {
                throw new Exception("Credentials are incorrect.");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, userDTO.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Credentials are incorrect.");
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            return true;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> RegisterAsync(RegisterUserDTO user)
        {
            if (user.Password != user.ConfirmPassword)
            {
                throw new Exception("Your password does not match.");
            }

            try
            {
                MailAddress email = new MailAddress(user.Email);
            }
            catch (FormatException)
            {
                throw new FormatException("The email address is not in a valid format.");
            }

            string pattern = @"^\+994(10|50|51|55|60|70|77|99)\d{7}$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(user.PhoneNumber))
            {
                throw new Exception("The phone number is not valid. Please ensure it follows the correct format. (+994501234567)");
            }

            AppUser appUser = _mapper.Map<AppUser>(user);
            var result = await _userManager.CreateAsync(appUser, user.Password);
            if (!result.Succeeded)
            {
                if (result.Errors.Any(e => e.Code == "DuplicateEmail"))
                {
                    throw new Exception("You have already been registered with this email.");
                }
                throw new Exception("Something went wrong.");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);

            var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var confirmationLink = $"{baseUrl}/api/accounts/confirm-email?userId={appUser.Id}&token={Uri.EscapeDataString(token)}";

            var emailBody = $"<p>Hello {user.FirstName},</p><p>Confirm your email: <a href='{confirmationLink}'>Click here</a></p>";
            await _emailService.SendEmailAsync(user.Email, "Confirm Email", emailBody);

            return true;
        }

        public async Task ChangePasswordAsync(string email, string password, string newPassword)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u=>u.Email==email);
            if (user is null)
            {
                throw new Exception("Couldn't find user.");
            }
            bool isValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isValid)
            {
                throw new Exception("Password is incorrect.");
            }
            IdentityResult result = await _userManager.ChangePasswordAsync(user,password,newPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Something went wrong. Please try again.");
            }
        }
    }
}
