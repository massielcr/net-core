using MediatR;
using PSDDDCQRSInPracticeLB.Api.Entities;
using PSDDDCQRSInPracticeLB.Api.Entities.ValueObjects;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.UpdateEmployeeSalary
{
    public sealed class UpdateEmployeeSalaryCommandHandler : IRequestHandler<UpdateEmployeeSalaryCommand>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IEmployeeSalaryHistoryRepository _salaryHistoryRepository;

        public UpdateEmployeeSalaryCommandHandler(IEmployeeRepository repository, IEmployeeSalaryHistoryRepository salaryHistoryRepository)
        {
            _repository = repository;
            _salaryHistoryRepository = salaryHistoryRepository;
        }

        public async Task Handle(UpdateEmployeeSalaryCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetById(request.employeeId);
            if (employee is null)
            {
                throw new KeyNotFoundException($"Employee with ID {request.employeeId} not found.");
            }

            //var salaryDto = request.salary;

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var currentSalary = employee.Salary;

            var salary = Money.Create(request.salary.Amount, request.salary.Currency);
            var updated = employee.UpdateSalary(salary, today);

            await _repository.Update(employee);

            if (updated)
            {
                // Add previous salary record to history if it exists
                if (currentSalary != null)
                {
                    await _salaryHistoryRepository.Add(new EmployeeSalaryHistory
                    {
                        EmployeeId = employee.Id,
                        Amount = currentSalary.Amount,
                        Currency = currentSalary.Currency,
                        From = currentSalary.From,
                        To = today
                    });
                }
            }
        }
    }
}
