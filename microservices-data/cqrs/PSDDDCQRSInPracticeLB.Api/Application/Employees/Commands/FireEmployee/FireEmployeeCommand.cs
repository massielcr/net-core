using PSDDDCQRSInPracticeLB.Api.Application.Common.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.FireEmployee
{
    public record FireEmployeeCommand(int EmployeeId) : ICommand;
}
