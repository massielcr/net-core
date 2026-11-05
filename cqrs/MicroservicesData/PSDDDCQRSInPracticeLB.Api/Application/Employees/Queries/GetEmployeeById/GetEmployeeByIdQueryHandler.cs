using MediatR;
using PSDDDCQRSInPracticeLB.Api.Dtos;
using PSDDDCQRSInPracticeLB.Api.Mapper;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Queries.GetEmployeeById
{
    public sealed class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDetailsDto>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDetailsDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetById(request.Id);
            if (employee is null)
            {
                throw new KeyNotFoundException($"Employee with ID {request.Id} not found.");
            }

            return EmployeeMapper.ToDetailsDto(employee);
        }
    }
}
