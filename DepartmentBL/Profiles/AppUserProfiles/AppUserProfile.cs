using AutoMapper;
using DepartmentBL.DTOs.UserDTO;
using DepartmentCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentBL.Profiles.AppUserProfiles
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, UserDTO>();
            CreateMap<RegisterUserDTO,AppUser>();
        }
    }
}
