using PSDDDCQRSInPracticeLB.Api.Dtos;
using PSDDDCQRSInPracticeLB.Api.Entities;
using AddressDto = PSDDDCQRSInPracticeLB.Api.Dtos.AddressDto;
using EmployeeSalaryDto = PSDDDCQRSInPracticeLB.Api.Dtos.EmployeeSalaryDto;
using StoreAssignmentDto = PSDDDCQRSInPracticeLB.Api.Dtos.StoreAssignmentDto;

namespace PSDDDCQRSInPracticeLB.Api.Mapper
{
    public static class EmployeeMapper
    {
        public static EmployeeDto ToDto(Employee employee)
        {
            return new EmployeeDto()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                IsDeleted = employee.IsDeleted,
                Salary = employee.Salary != null ? ToDto(employee.Salary) : null,
                StoreAssignments = employee.StoreAssignments?.Select(ToDto).ToList()
            };
        }

        public static EmployeeDetailsDto ToDetailsDto(Employee employee)
        {
            return new EmployeeDetailsDto()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                DateOfBirth = employee.DateOfBirth,
                Address = employee.Address != null ? ToDto(employee.Address) : null,
                StartDate = employee.StartDate,
                EndDate = employee.EndDate,
                IsDeleted = employee.IsDeleted,
                Salary = employee.Salary != null ? ToDto(employee.Salary) : null,
                StoreAssignments = employee.StoreAssignments?.Select(ToDto).ToList()
            };
        }

        public static AddressDto ToDto(Address address)
        {
            return new AddressDto()
            {
                Id = address.Id,
                Street = address.Street,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode,
                Country = address.Country
            };
        }

        private static EmployeeSalaryDto ToDto(EmployeeSalary salary)
        {
            return new EmployeeSalaryDto()
            {
                Id = salary.Id,
                Amount = salary.Amount,
                Currency = salary.Currency,
                From = salary.From,
            };
        }

        private static StoreAssignmentDto ToDto(StoreAssignment assignment)
        {
            return new StoreAssignmentDto()
            {
                Id = assignment.Id,
                StoreId = assignment.StoreId,
                Store = StoreMapper.ToDto(assignment.Store),
                JobRoleId = assignment.JobRoleId,
                JobRole = JobRoleMapper.ToDto(assignment.JobRole),
                From = assignment.From
            };
        }
    }
}
