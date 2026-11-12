using MediatR;
using PSDDDCQRSInPracticeLB.Api.Entities;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.RemoveStoreAssignment
{
    public sealed class RemoveStoreAssignmentCommandHandler : IRequestHandler<RemoveStoreAssignmentCommand>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IEmployeeStoreAssignmentHistoryRepository _storeAssignmentHistoryRepository;

        public RemoveStoreAssignmentCommandHandler(IEmployeeRepository repository, IEmployeeStoreAssignmentHistoryRepository storeAssignmentHistoryRepository)
        {
            _repository = repository;
            _storeAssignmentHistoryRepository = storeAssignmentHistoryRepository;
        }

        public async Task Handle(RemoveStoreAssignmentCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetById(request.EmployeeId);
            if (employee is null)
            {
                throw new KeyNotFoundException($"Employee with ID {request.EmployeeId} not found.");
            }

            var assignment = employee.StoreAssignments.FirstOrDefault(sa => sa.StoreId == request.StoreId);
            if (assignment is null)
            {
                throw new KeyNotFoundException($"No active assignment for employee {request.EmployeeId} at store {request.StoreId}.");
            }

            employee.StoreAssignments.Remove(assignment);

            await _storeAssignmentHistoryRepository.Add(new StoreAssignmentHistory
            {
                EmployeeId = employee.Id,
                StoreId = assignment.StoreId,
                JobRoleId = assignment.JobRoleId,
                From = assignment.From,
                To = DateOnly.FromDateTime(DateTime.UtcNow)
            });

            await _repository.Update(employee);
        }
    }
}
