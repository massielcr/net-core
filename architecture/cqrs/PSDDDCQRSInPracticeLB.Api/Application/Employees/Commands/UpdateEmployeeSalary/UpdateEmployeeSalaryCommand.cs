using PSDDDCQRSInPracticeLB.Api.Application.Common.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.UpdateEmployeeSalary
{
    public sealed record UpdateEmployeeSalaryCommand(int employeeId, UpdateEmployeeSalaryDto salary) : ICommand;
}
