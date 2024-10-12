using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrgHierarchyAPI.DTOs;
using OrgHierarchyAPI.Models;
using OrgHierarchyAPI.Repository;

namespace OrgHierarchyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeeDtos);
        }

        // GET: api/employees/position/{positionId}
        [HttpGet("position/{positionId}")]
        public async Task<IActionResult> GetEmployeesByPositionId(Guid positionId)
        {
            var employees = await _employeeRepository.GetEmployeesByPositionIdAsync(positionId);
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeeDtos);
        }

        // POST: api/employees
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeCreateDto employeeDto)
        {
            // Map the incoming DTO to the Employee entity
            var employee = _mapper.Map<Employee>(employeeDto);

            // Add the employee to the repository
            var createdEmployee = await _employeeRepository.AddEmployeeAsync(employee);

            // Map the newly created employee back to the EmployeeDto to return
            var createdEmployeeDto = _mapper.Map<EmployeeDto>(createdEmployee);

            // Return the newly created employee's details
            return CreatedAtAction(nameof(GetEmployeesByPositionId), new { positionId = createdEmployee.PositionId }, createdEmployeeDto);
        }

        // PUT: api/employees/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeDto employeeDto)
        {
            if (id != employeeDto.Id)
            {
                return BadRequest();
            }

            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.UpdateEmployeeAsync(employee);

            return NoContent();
        }

        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}




