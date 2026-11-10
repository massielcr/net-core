using PSDDDCQRSInPracticeLB.Api.Application.Common.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.HireEmployee
{
    public record HireEmployeeCommand(HireEmployeeDto Employee) : ICommand<int>;
}
