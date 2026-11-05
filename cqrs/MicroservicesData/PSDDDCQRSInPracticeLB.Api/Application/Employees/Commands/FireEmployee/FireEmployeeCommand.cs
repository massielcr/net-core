using MediatR;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.FireEmployee
{
    public record FireEmployeeCommand(int EmployeeId) : IRequest;
}
