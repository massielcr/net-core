using Microsoft.Identity.Client.NativeInterop;
using PSDDDCQRSInPracticeLB.Api.Dtos;
using PSDDDCQRSInPracticeLB.Api.Entities;

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
                PhoneNumber = employee.PhoneNumber,
                DateOfBirth = employee.DateOfBirth,
                Address = employee.Address != null ? ToDto(employee.Address) : null,
                StartDate = employee.StartDate,
                EndDate = employee.EndDate,
                IsDeleted = employee.IsDeleted,
                Salary = employee.Salaries.FirstOrDefault(s => s.To == null) != null ? ToDto(employee.Salaries.FirstOrDefault(s => s.To == null)!) : null,
                StoreAssignments = employee.StoreAssignments?.Select(ToDto).ToList()
            };
        }

        public static Employee ToEntity(EmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                DateOfBirth = employeeDto.DateOfBirth,
                Address = employeeDto.Address != null ? ToEntity(employeeDto.Address) : null,
                StartDate = employeeDto.StartDate,
                EndDate = employeeDto.EndDate,
                IsDeleted = employeeDto.IsDeleted,
                Salaries = (employeeDto.Salary == null ? null : new[] { ToEntity(employeeDto.Salary) }) ?? Array.Empty<EmployeeSalary>(),
                StoreAssignments = employeeDto.StoreAssignments?.Select(ToEntity).ToList() ?? new List<StoreAssignment>()
            };

            if (employeeDto.Id is not null && employeeDto.Id > 0)
            {
                employee.Id = (int)employeeDto.Id;
            }

            return employee;
        }        

        public static void MapOnto(Employee employee, EmployeeDto dto)
        {
            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Email = dto.Email;
            employee.PhoneNumber = dto.PhoneNumber;
            employee.DateOfBirth = dto.DateOfBirth;
            employee.StartDate = dto.StartDate;
            employee.EndDate = dto.EndDate;
            employee.IsDeleted = dto.IsDeleted;

            if (dto.Address is not null)
            {
                if (employee.Address is null)
                {
                    employee.Address = ToEntity(dto.Address);
                }
                else
                {
                    employee.Address.Street     = dto.Address.Street;
                    employee.Address.City       = dto.Address.City;
                    employee.Address.State      = dto.Address.State;
                    employee.Address.PostalCode = dto.Address.PostalCode;
                    employee.Address.Country    = dto.Address.Country;
                }
            }
            else
            {
                employee.Address = null;
            }

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var dtoAssignments = dto.StoreAssignments ?? new List<StoreAssignmentDto>();

            if (HasStoreAssignmentsChanged(dtoAssignments.ToList(), employee.StoreAssignments.ToList()))
            {
                var storeAssignmentsToRemove = employee.StoreAssignments.Where(sa => !dtoAssignments?.Any(dtosa => dtosa.Id == sa.Id) ?? false).ToList();

                foreach(var assignment in storeAssignmentsToRemove)
                {
                    assignment.To = today;
                }

                var storeAssignmentsToAdd = dtoAssignments.Where(sa => sa.Id == null);

                foreach(var assignmentDto in storeAssignmentsToAdd)
                {
                    employee.StoreAssignments.Add(ToEntity(assignmentDto));
                }
            }

            if (HasSalaryChanged(dto?.Salary, employee.Salaries.ToList()))
            {
                var salariesToEnd = employee.Salaries.Where(s => s.To == null).ToList();

                foreach(var salary in salariesToEnd)
                {
                    salary.To = today;
                }

                if (dto?.Salary != null)
                {
                    dto.Salary.Id = null;
                    employee.Salaries.Add(ToEntity(dto.Salary));
                }
            }
        }

        private static bool HasStoreAssignmentsChanged(List<StoreAssignmentDto> employeeDtoStoreAssignments, List<StoreAssignment> employeeStoreAssignments)
        {
            return employeeDtoStoreAssignments.Any(sa => sa.Id == null) ||
                   employeeDtoStoreAssignments.Count != employeeStoreAssignments.Count ||
                   employeeDtoStoreAssignments.Any(sa => !employeeDtoStoreAssignments.Contains(sa));
        }

        private static bool HasSalaryChanged(EmployeeSalaryDto? dtoSalary, List<EmployeeSalary> salaries)
        {
            var salary = salaries.FirstOrDefault(s => s.To == null);
            return dtoSalary?.Amount != salary?.Amount ||
                   dtoSalary?.Currency != salary?.Currency;
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

        public static Address ToEntity(AddressDto addressDto)
        {
            var address = new Address
            {
                Street = addressDto.Street,
                City = addressDto.City,
                State = addressDto.State,
                PostalCode = addressDto.PostalCode,
                Country = addressDto.Country
            };

            if (addressDto.Id is not null && addressDto.Id > 0)
            {
                address.Id = (int)addressDto.Id;
            }

            return address;
        }

        public static EmployeeSalaryDto ToDto(EmployeeSalary salary)
        {
            return new EmployeeSalaryDto(){
                Id = salary.Id,
                Amount = salary.Amount,
                Currency = salary.Currency,
                From = salary.From,
                To = salary.To
            };
        }

        public static EmployeeSalary ToEntity(EmployeeSalaryDto salaryDto)
        {
            var salary = new EmployeeSalary
            {
                Amount = salaryDto.Amount,
                Currency = salaryDto.Currency,
                From = salaryDto.From,
                To = salaryDto.To
            };

            if (salaryDto.Id is not null && salaryDto.Id > 0)
            {
                salary.Id = (int)salaryDto.Id;
            }

            return salary;
        }

        public static StoreAssignmentDto ToDto(StoreAssignment assignment)
        {
            return new StoreAssignmentDto()
            {
                Id = assignment.Id,
                StoreId = assignment.StoreId,
                Store = StoreMapper.ToDto(assignment.Store),
                JobRoleId = assignment.JobRoleId,
                JobRole = JobRoleMapper.ToDto(assignment.JobRole),
                From = assignment.From,
                To = assignment.To
            };
        }

        public static StoreAssignment ToEntity(StoreAssignmentDto assignmentDto)
        {
            var assignment = new StoreAssignment
            {
                StoreId = assignmentDto.StoreId,
                JobRoleId = assignmentDto.JobRoleId,
                From = assignmentDto.From,
                To = assignmentDto.To
            };

            if (assignmentDto.Id is not null && assignmentDto.Id > 0)
            {
                assignment.Id = (int)assignmentDto.Id;
            }

            return assignment;
        }        
    }
}
