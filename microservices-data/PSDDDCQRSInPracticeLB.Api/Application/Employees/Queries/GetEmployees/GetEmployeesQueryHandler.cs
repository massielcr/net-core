using MediatR;
using PSDDDCQRSInPracticeLB.Api.Dtos;
using PSDDDCQRSInPracticeLB.Api.Mapper;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Queries.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeDto>>
    {
        private readonly IEmployeeRepository _repository;

        public GetEmployeesQueryHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _repository.GetAll();
            return employees.Select(EmployeeMapper.ToDto).ToList();
        }
    }
}
