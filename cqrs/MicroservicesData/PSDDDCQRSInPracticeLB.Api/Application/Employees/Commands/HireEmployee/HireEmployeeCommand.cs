using MediatR;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.HireEmployee
{
    public record HireEmployeeCommand(HireEmployeeDto Employee) : IRequest<int>;
}
