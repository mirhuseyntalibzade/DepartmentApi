using BusinessLayer.Services.Abstractions;
using DepartmentBL.DTOs.EmployeeDTO;
using DepartmentCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please fill all inputs");
            }
            try
            {
                await _employeeService.AddEmployeeAsync(employeeDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeDTO employeeDTO,int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please fill all inputs");
            }
            try
            {
                await _employeeService.UpdateEmployeeAsync(employeeDTO,Id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteEmployee(int Id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(Id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
