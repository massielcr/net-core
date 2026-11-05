using MediatR;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.ChangeEmployeeRoleInStore
{
    public record ChangeEmployeeRoleInStoreCommand(int EmployeeId, int StoreId, ChangeEmployeeRoleInStore ChangeRole) : IRequest;
}
