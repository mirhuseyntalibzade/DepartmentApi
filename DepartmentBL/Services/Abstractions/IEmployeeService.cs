using DepartmentBL.DTOs.EmployeeDTO;
using DepartmentCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstractions
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(CreateEmployeeDTO employee);
        Task UpdateEmployeeAsync(UpdateEmployeeDTO employee, int Id);
        Task DeleteEmployeeAsync(int Id);
    }
}
