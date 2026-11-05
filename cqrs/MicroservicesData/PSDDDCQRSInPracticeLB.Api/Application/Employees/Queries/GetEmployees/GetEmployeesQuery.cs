using MediatR;
using PSDDDCQRSInPracticeLB.Api.Dtos;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Queries.GetEmployees
{
    public sealed record GetEmployeesQuery() : IRequest<List<EmployeeDto>>;
}
