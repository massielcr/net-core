using MediatR;
using PSDDDCQRSInPracticeLB.Api.Entities;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.ChangeEmployeeRoleInStore
{
    public class ChangeEmployeeRoleInStoreCommandHandler : IRequestHandler<ChangeEmployeeRoleInStoreCommand>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IEmployeeStoreAssignmentHistoryRepository _storeAssignmentHistoryRepository;

        public ChangeEmployeeRoleInStoreCommandHandler(IEmployeeRepository repository, IEmployeeStoreAssignmentHistoryRepository storeAssignmentHistoryRepository)
        {
            _repository = repository;
            _storeAssignmentHistoryRepository = storeAssignmentHistoryRepository;
        }

        public async Task Handle(ChangeEmployeeRoleInStoreCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetById(request.EmployeeId);
            if (employee is null)
            {
                throw new KeyNotFoundException($"Employee with ID {request.EmployeeId} not found.");
            }

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var oldAssignment = employee.StoreAssignments.FirstOrDefault(sa => sa.StoreId == request.StoreId);

            var updated = employee.ChangeEmployeeRoleInStore(request.StoreId, request.ChangeRole.JobRoleId, today);
            if (oldAssignment is not null && updated)
            {
                await _storeAssignmentHistoryRepository.Add(new StoreAssignmentHistory
                {
                    EmployeeId = employee.Id,
                    StoreId = oldAssignment.StoreId,
                    JobRoleId = oldAssignment.JobRoleId,
                    From = oldAssignment.From,
                    To = today
                });
            }

            await _repository.Update(employee);
        }
    }
}
