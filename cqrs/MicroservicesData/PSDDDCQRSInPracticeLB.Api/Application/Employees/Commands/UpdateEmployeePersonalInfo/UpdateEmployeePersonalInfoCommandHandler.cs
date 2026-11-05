using MediatR;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.UpdateEmployeePersonalInfo
{
    public sealed class UpdateEmployeePersonalInfoCommandHandler : IRequestHandler<UpdateEmployeePersonalInfoCommand>
    {
        private readonly IEmployeeRepository _repository;

        public UpdateEmployeePersonalInfoCommandHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateEmployeePersonalInfoCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetById(request.EmployeeId);

            if (employee is null)
            {
                throw new KeyNotFoundException($"Employee with id {request.EmployeeId} not found.");
            }

            employee.UpdatePersonalInfo(
                request.PersonalInfoDto.FirstName,
                request.PersonalInfoDto.LastName,
                request.PersonalInfoDto.Email,
                request.PersonalInfoDto.PhoneNumber,
                request.PersonalInfoDto.DateOfBirth,
                request.PersonalInfoDto.StartDate);

            employee.UpdateAddress(
                request.PersonalInfoDto.Address.Street,
                request.PersonalInfoDto.Address.PostalCode,
                request.PersonalInfoDto.Address.City,
                request.PersonalInfoDto.Address.State,
                request.PersonalInfoDto.Address.Country);

            await _repository.Update(employee);
        }
    }
}
