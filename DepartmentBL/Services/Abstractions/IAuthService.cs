using DepartmentBL.DTOs.UserDTO;
using DepartmentCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentBL.Services.Abstractions
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterUserDTO user);
        Task<bool> ConfirmEmailAsync(string userId, string token);
        Task<bool> LoginAsync(LoginUserDTO user);
        Task ForgotPasswordAsync(string email);
        Task ResetPasswordAsync(string email, string token, string newPassword);
        Task ChangePasswordAsync(string email, string password, string newPassword);
        Task LogoutAsync();
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetOneUserAsync(string userName);
    }
}
