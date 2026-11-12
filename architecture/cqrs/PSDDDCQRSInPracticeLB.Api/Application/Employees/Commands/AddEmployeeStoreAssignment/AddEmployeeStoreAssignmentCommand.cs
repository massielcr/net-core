using PSDDDCQRSInPracticeLB.Api.Application.Common.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.AddEmployeeStoreAssignment
{
    public sealed record AddEmployeeStoreAssignmentCommand(int EmployeeId, AddEmployeeStoreAssignmentDto StoreAssignment) : ICommand;
}
