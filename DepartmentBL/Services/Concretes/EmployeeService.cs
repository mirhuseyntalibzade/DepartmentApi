using AutoMapper;
using BusinessLayer.Services.Abstractions;
using DepartmentBL.DTOs.EmployeeDTO;
using DepartmentCore.Models;
using DepartmentDAL.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        readonly IEmployeeRepository _repository;
        readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddEmployeeAsync(CreateEmployeeDTO employeeDTO)
        {
            Employee employee = _mapper.Map<Employee>(employeeDTO);
            employee.CreatedBy = employeeDTO.CreatedBy;
            employee.CreatedDate = DateTime.UtcNow.AddHours(4);
            await _repository.Create(employee);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int Id)
        {
            Employee deletedEmployee = await _repository.FindOneAsync(d => d.Id == Id);
            if (deletedEmployee is null)
            {
                throw new Exception("Employee cannot be null");
            }
            _repository.Delete(deletedEmployee);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employee = await _repository.FindByIdAsync(id);
            if (employee == null)
            {
                throw new Exception($"Employee with id {id} not found.");
            }
            return employee;
        }

        public async Task UpdateEmployeeAsync(UpdateEmployeeDTO employeeDTO, int Id)
        {
            Employee currentEmployee = await _repository.FindOneAsync(d => d.Id == Id);
            if (currentEmployee is null)
            {
                throw new Exception("Cannot find employee");
            }
            Employee updatedEmployee = _mapper.Map<Employee>(employeeDTO);
            updatedEmployee.Id = Id;
            updatedEmployee.UpdatedBy = employeeDTO.UpdatedBy;
            updatedEmployee.UpdatedDate = DateTime.UtcNow.AddHours(4);
            updatedEmployee.CreatedBy = currentEmployee.CreatedBy;
            updatedEmployee.CreatedDate = currentEmployee.CreatedDate;

            _repository.Update(updatedEmployee);
            await _repository.SaveChangesAsync();
        }
    }
}
