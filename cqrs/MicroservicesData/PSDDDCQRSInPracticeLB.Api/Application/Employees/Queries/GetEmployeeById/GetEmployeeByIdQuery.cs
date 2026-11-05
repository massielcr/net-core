using MediatR;
using PSDDDCQRSInPracticeLB.Api.Dtos;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Queries.GetEmployeeById
{
    public sealed record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeDetailsDto>;
}
