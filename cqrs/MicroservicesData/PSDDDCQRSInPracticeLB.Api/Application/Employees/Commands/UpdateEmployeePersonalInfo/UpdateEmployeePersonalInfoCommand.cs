using MediatR;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.UpdateEmployeePersonalInfo
{
    public sealed record UpdateEmployeePersonalInfoCommand(int EmployeeId, UpdateEmployeePersonalInfoDto PersonalInfoDto) : IRequest;
}
