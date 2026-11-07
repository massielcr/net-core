using MediatR;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.AddEmployeeStoreAssignment
{
    public sealed class AddEmployeeStoreAssignmentCommandHandler : IRequestHandler<AddEmployeeStoreAssignmentCommand>
    {
        private readonly IEmployeeRepository _repository;

        public AddEmployeeStoreAssignmentCommandHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(AddEmployeeStoreAssignmentCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetById(request.EmployeeId);
            if (employee is null)
            {
                throw new KeyNotFoundException($"Employee with id {request.EmployeeId} not found.");
            }

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            employee.AssignStoreAssignment(
                request.StoreAssignment.StoreId,
                request.StoreAssignment.JobRoleId,
                today);

            await _repository.Update(employee);
        }
    }
}
