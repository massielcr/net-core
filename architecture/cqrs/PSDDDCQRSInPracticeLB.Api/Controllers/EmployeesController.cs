using MediatR;
using Microsoft.AspNetCore.Mvc;
using PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.AddEmployeeStoreAssignment;
using PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.ChangeEmployeeRoleInStore;
using PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.FireEmployee;
using PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.HireEmployee;
using PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.RemoveStoreAssignment;
using PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.UpdateEmployeePersonalInfo;
using PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.UpdateEmployeeSalary;
using PSDDDCQRSInPracticeLB.Api.Application.Employees.Queries.GetEmployeeById;
using PSDDDCQRSInPracticeLB.Api.Application.Employees.Queries.GetEmployees;
using PSDDDCQRSInPracticeLB.Api.Dtos;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PSDDDCQRSInPracticeLB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IEmployeeRepository employeeRepository, IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<List<EmployeeDto>> GetAll()
        {
            return await _mediator.Send(new GetEmployeesQuery());
        }


        [HttpGet("{id:int}")]
        public async Task<EmployeeDetailsDto> Get([FromRoute] int id)
        {
            return await _mediator.Send(new GetEmployeeByIdQuery(id));
        }


        [HttpPost]
        public async Task<int> HireEmployee([FromBody] HireEmployeeDto employeeDto)
        {
            ArgumentNullException.ThrowIfNull(employeeDto);
            ArgumentNullException.ThrowIfNull(employeeDto.Salary);
            ArgumentNullException.ThrowIfNull(employeeDto.StoreAssignments);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(employeeDto.StoreAssignments?.Count() ?? 0, 2);

            return await _mediator.Send(new HireEmployeeCommand(employeeDto));
        }


        [HttpPut("{id:int}/personal-info")]
        public async Task<IActionResult> UpdatePersonalInformation([FromRoute] int id, [FromBody] UpdateEmployeePersonalInfoDto dto)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(id, 1);
            ArgumentNullException.ThrowIfNull(dto);

            try
            {
                await _mediator.Send(new UpdateEmployeePersonalInfoCommand(id, dto));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }


        [HttpPost("{id:int}/salary")]
        public async Task<IActionResult> AddSalary([FromRoute] int id, [FromBody] UpdateEmployeeSalaryDto dto)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(id, 1);
            ArgumentNullException.ThrowIfNull(dto);

            try
            {
                await _mediator.Send(new UpdateEmployeeSalaryCommand(id, dto));
            }
            catch(KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }


        [HttpPost("{id:int}/assignments")]
        public async Task<IActionResult> AddAssignment([FromRoute] int id, [FromBody] AddEmployeeStoreAssignmentDto dto)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(id, 1);
            ArgumentNullException.ThrowIfNull(dto);

            try
            {
                await _mediator.Send(new AddEmployeeStoreAssignmentCommand(id, dto));
            }
            catch(KeyNotFoundException)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }


        [HttpPut("{id:int}/store/{storeId:int}/role")]
        public async Task<IActionResult> ChangeAssignmentRole([FromRoute] int id, [FromRoute] int storeId, [FromBody] ChangeEmployeeRoleInStore dto)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(id, 1);
            ArgumentOutOfRangeException.ThrowIfLessThan(storeId, 1);
            ArgumentNullException.ThrowIfNull(dto);

            try
            {
                await _mediator.Send(new ChangeEmployeeRoleInStoreCommand(id, storeId, dto));
            }
            catch(KeyNotFoundException)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }


        [HttpDelete("{id:int}/store/{storeId:int}")]
        public async Task<IActionResult> RemoveAssignment([FromRoute] int id, [FromRoute] int storeId)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(id, 1);
            ArgumentOutOfRangeException.ThrowIfLessThan(storeId, 1);

            try
            {
                await _mediator.Send(new RemoveStoreAssignmentCommand(id, storeId));
            }
            catch(KeyNotFoundException)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> FireEmployee([FromRoute] int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(id, 1);

            try
            {
                await _mediator.Send(new FireEmployeeCommand(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
