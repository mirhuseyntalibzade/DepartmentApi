using AutoMapper;
using BusinessLayer.Services.Abstractions;
using DepartmentBL.DTOs.DepartmentDTO;
using DepartmentCore.Models;
using DepartmentDAL.Repositories.Abstractions;
using DepartmentDAL.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        readonly IDepartmentRepository _repository;
        readonly IMapper _mapper;
        public DepartmentService(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddDepartmentAsync(CreateDepartmentDTO departmentDTO)
        {
            Department department = _mapper.Map<Department>(departmentDTO);
            department.CreatedBy = departmentDTO.CreatedBy;
            department.CreatedDate = DateTime.UtcNow.AddHours(4);
            await _repository.Create(department);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(int Id)
        {
            Department deletedDepartment = await _repository.FindOneAsync(d => d.Id == Id);
            if (deletedDepartment is null)
            {
                throw new Exception("Department cannot be null");
            }
            _repository.Delete(deletedDepartment);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var department = await _repository.FindByIdAsync(id);
            if (department == null)
            {
                throw new Exception($"Employee with id {id} not found.");
            }
            return department;
        }

        public async Task UpdateDepartmentAsync(UpdateDepartmentDTO departmentDTO, int Id)
        {
            Department currentDepartment = await _repository.FindOneAsync(d => d.Id == Id);
            if (currentDepartment is null)
            {
                throw new Exception("Cannot find department");
            }
            Department department = _mapper.Map<Department>(departmentDTO);
            department.Id = Id;
            department.CreatedBy = currentDepartment.CreatedBy;
            department.CreatedDate = currentDepartment.CreatedDate;
            department.UpdatedBy = departmentDTO.UpdatedBy;
            department.UpdatedDate = DateTime.UtcNow.AddHours(4);
            _repository.Update(department);
            await _repository.SaveChangesAsync();
        }
    }
}
