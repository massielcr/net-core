using MediatR;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.AddEmployeeStoreAssignment
{
    public sealed record AddEmployeeStoreAssignmentCommand(int EmployeeId, AddEmployeeStoreAssignmentDto StoreAssignment) : IRequest;
}
