using FluentValidation;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.AddEmployeeStoreAssignment
{
    public class AddEmployeeStoreAssignmentCommandValidator : AbstractValidator<AddEmployeeStoreAssignmentCommand>
    {
        private readonly IJobRoleRepository _jobRoleRepository;
        private readonly IStoreRepository _storeRepository;

        public AddEmployeeStoreAssignmentCommandValidator(IJobRoleRepository jobRoleRepository, IStoreRepository storeRepository)
        {
            _jobRoleRepository = jobRoleRepository;
            _storeRepository = storeRepository;

            RuleFor(x => x.EmployeeId)
                .GreaterThan(0)
                .WithMessage("Employee ID must be greater than 0");

            RuleFor(x => x.StoreAssignment)
                .NotNull()
                .WithMessage("Store assignment is required");

            RuleFor(x => x.StoreAssignment.StoreId)
                .GreaterThan(0)
                .WithMessage("Store ID must be greater than 0")
                .MustAsync(BeValidStore)
                .WithMessage("Store does not exist");

            RuleFor(x => x.StoreAssignment.JobRoleId)
                .GreaterThan(0)
                .WithMessage("Job Role ID must be greater than 0")
                .MustAsync(BeValidJobRole)
                .WithMessage("Job role does not exist");
        }

        private async Task<bool> BeValidStore(int storeId, CancellationToken cancellationToken)
        {
            return await _storeRepository.ExistsById(storeId);
        }

        private async Task<bool> BeValidJobRole(int jobRoleId, CancellationToken cancellationToken)
        {
            return await _jobRoleRepository.ExistById(jobRoleId);
        }
    }
}
