using DepartmentBL.DTOs.DepartmentDTO;
using DepartmentCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstractions
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int id);
        Task AddDepartmentAsync(CreateDepartmentDTO department);
        Task UpdateDepartmentAsync(UpdateDepartmentDTO department, int Id);
        Task DeleteDepartmentAsync(int Id);
    }
}
