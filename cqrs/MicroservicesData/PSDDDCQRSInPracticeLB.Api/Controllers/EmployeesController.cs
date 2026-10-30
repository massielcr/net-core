using Microsoft.AspNetCore.Mvc;
using PSDDDCQRSInPracticeLB.Api.Dtos;
using PSDDDCQRSInPracticeLB.Api.Mapper;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PSDDDCQRSInPracticeLB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        [HttpGet]
        public async Task<List<EmployeeDto>> GetAll()
        {
            var employees = await _employeeRepository.GetAll();
            return employees.Select(EmployeeMapper.ToDto).ToList();
        }


        [HttpGet("{id}")]
        public async Task<EmployeeDto> Get([FromRoute] int id)
        {
            var employee = await _employeeRepository.GetById(id);
            if (employee is null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }

            return EmployeeMapper.ToDto(employee);
        }


        [HttpPost]
        public async Task<int> Create([FromBody] EmployeeDto employeeDto)
        {
            ArgumentNullException.ThrowIfNull(employeeDto);
            ArgumentNullException.ThrowIfNull(employeeDto.Salary);
            ArgumentNullException.ThrowIfNull(employeeDto.StoreAssignments);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(employeeDto.StoreAssignments?.Count() ?? 0, 2);

            var employee = EmployeeMapper.ToEntity(employeeDto);
            var createdEmployee = await _employeeRepository.Create(employee);
            return createdEmployee.Id;
        }


        [HttpPut("{id}")]
        public async Task<int> Update([FromRoute] int id, [FromBody] EmployeeDto employeeDto)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(id, 1);
            ArgumentNullException.ThrowIfNull(employeeDto);
            ArgumentNullException.ThrowIfNull(employeeDto.Salary);
            ArgumentNullException.ThrowIfNull(employeeDto.StoreAssignments);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(employeeDto.StoreAssignments?.Count() ?? 0, 2);

            var employee = await _employeeRepository.GetById(id);
            if (employee is null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found");
            }

            EmployeeMapper.MapOnto(employee, employeeDto);
            await _employeeRepository.Update(employee);
            return employee.Id;
        }


        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(id, 1);

            var employee = await _employeeRepository.GetById(id);
            if (employee is null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found");
            }

            await _employeeRepository.Delete(employee);
        }
    }
}
