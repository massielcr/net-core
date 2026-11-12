using PSDDDCQRSInPracticeLB.Api.Application.Common.Interfaces;
using PSDDDCQRSInPracticeLB.Api.Dtos;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Queries.GetEmployees
{
    public sealed record GetEmployeesQuery() : IQuery<List<EmployeeDto>>;
}
