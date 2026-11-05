using PSDDDCQRSInPracticeLB.Api.Application.Common.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.RemoveStoreAssignment
{
    public record RemoveStoreAssignmentCommand(int EmployeeId, int StoreId) : ICommand;
}
