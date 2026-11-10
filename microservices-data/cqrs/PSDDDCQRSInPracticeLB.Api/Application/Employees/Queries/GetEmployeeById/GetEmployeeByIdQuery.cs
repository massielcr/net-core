using PSDDDCQRSInPracticeLB.Api.Application.Common.Attributes;
using PSDDDCQRSInPracticeLB.Api.Application.Common.Interfaces;
using PSDDDCQRSInPracticeLB.Api.Dtos;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Queries.GetEmployeeById
{
    [DisableLoggin]
    public sealed record GetEmployeeByIdQuery(int Id) : IQuery<EmployeeDetailsDto>;
}
