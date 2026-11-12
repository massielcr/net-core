using MediatR;
using PSDDDCQRSInPracticeLB.Api.Entities;
using PSDDDCQRSInPracticeLB.Api.Entities.ValueObjects;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.HireEmployee
{
    public sealed class HireEmployeeCommandHandler : IRequestHandler<HireEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _repository;

        public HireEmployeeCommandHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(HireEmployeeCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Employee;

            // Validate required collections for initial creation
            if (dto.Address == null)
            {
                throw new ArgumentException("Address entry is required when creating an employee.");
            }

            if (dto.Salary == null)
            {
                throw new ArgumentException("Salary entry is required when creating an employee.");
            }

            if (dto.StoreAssignments == null)
            {
                throw new ArgumentException("Store assignments collection must be provided.");
            }

            var emailAddress = EmailAddress.Create(request.Employee.Email);
            var salary = Money.Create(request.Employee.Salary!.Amount, request.Employee.Salary!.Currency);
            var employee = new Employee(
                request.Employee.FirstName,
                request.Employee.LastName,
                emailAddress,
                request.Employee.PhoneNumber,
                request.Employee.DateOfBirth,
                request.Employee.StartDate,
                request.Employee.Address!.Street,
                request.Employee.Address!.PostalCode,
                request.Employee.Address!.City,
                request.Employee.Address!.State,
                request.Employee.Address!.Country,
                salary);

            if (request.Employee.StoreAssignments != null)
            {
                foreach (var assignment in request.Employee.StoreAssignments)
                {
                    employee.AssignStoreAssignment(assignment.StoreId, assignment.JobRoleId,
                        DateOnly.FromDateTime(DateTime.UtcNow));
                }
            }

            await _repository.Create(employee);

            return employee.Id;
        }
    }
}
