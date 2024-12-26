using AutoMapper;
using DepartmentBL.DTOs.EmployeeDTO;
using DepartmentCore.Models;

namespace DepartmentBL.Profiles.EmployeeProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, CreateEmployeeDTO>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeDTO>().ReverseMap();
        }
    }
}
