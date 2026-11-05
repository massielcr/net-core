using MediatR;
using PSDDDCQRSInPracticeLB.Api.Entities;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.FireEmployee
{
    public sealed class FireEmployeeCommandHandler : IRequestHandler<FireEmployeeCommand>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IEmployeeStoreAssignmentHistoryRepository _storeAssignmentHistoryRepository;
        private readonly IEmployeeSalaryHistoryRepository _employeeSalaryHistoryRepository;

        public FireEmployeeCommandHandler(IEmployeeRepository repository,
                                          IEmployeeStoreAssignmentHistoryRepository storeAssignmentHistoryRepository,
                                          IEmployeeSalaryHistoryRepository employeeSalaryHistoryRepository)
        {
            _repository = repository;
            _storeAssignmentHistoryRepository = storeAssignmentHistoryRepository;
            _employeeSalaryHistoryRepository = employeeSalaryHistoryRepository;
        }

        public async Task Handle(FireEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetById(request.EmployeeId);
            if (employee is null)
            {
                throw new KeyNotFoundException($"Employee with ID {request.EmployeeId} not found.");
            }

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var assignments = employee.StoreAssignments.ToList();
            var salary = employee.Salary;

            employee.Fire(today);

            foreach (var assignment in assignments)
            {
                await _storeAssignmentHistoryRepository.Add(new StoreAssignmentHistory
                {
                    EmployeeId = employee.Id,
                    StoreId = assignment.StoreId,
                    JobRoleId = assignment.JobRoleId,
                    From = assignment.From,
                    To = today
                });
            }

            if (salary is not null)
            {
                await _employeeSalaryHistoryRepository.Add(new EmployeeSalaryHistory
                {
                    EmployeeId = employee.Id,
                    Amount = salary.Amount,
                    From = salary.From,
                    To = today
                });
            }

            await _repository.Update(employee);
        }
    }
}
