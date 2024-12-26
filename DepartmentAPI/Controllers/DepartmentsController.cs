using BusinessLayer.Services.Abstractions;
using DepartmentBL.DTOs.DepartmentDTO;
using DepartmentCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = await _departmentService.GetDepartmentByIdAsync(id);
                return Ok(department);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] CreateDepartmentDTO departmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please fill all inputs");
            }
            try
            {
                await _departmentService.AddDepartmentAsync(departmentDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentDTO department, int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please fill all inputs");
            }
            try
            {
                await _departmentService.UpdateDepartmentAsync(department, Id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteDepartment(int Id)
        {
            try
            {
                await _departmentService.DeleteDepartmentAsync(Id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
