using MediatR;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.RemoveStoreAssignment
{
    public record RemoveStoreAssignmentCommand(int EmployeeId, int StoreId) : IRequest;
}
