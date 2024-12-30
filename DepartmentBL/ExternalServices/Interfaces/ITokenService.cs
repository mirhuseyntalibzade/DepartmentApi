using DepartmentCore.Models;

namespace DepartmentBL.ExternalServices.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(AppUser appUser);
    }
}
