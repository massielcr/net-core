using PSDDDCQRSInPracticeLB.Api.Entities.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSDDDCQRSInPracticeLB.Api.Entities
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public DateOnly DateOfBirth { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly? EndDate { get; private set; }
        public bool IsDeleted { get; private set; }        
        public int? AddressId { get; private set; }
        public Address? Address { get; private set; }
        public ICollection<StoreAssignment> StoreAssignments { get; private set; } = new List<StoreAssignment>();
        public EmployeeSalary? Salary { get; private set; }

        public Employee() { }

        public Employee(string firstName, string lastName, EmailAddress email, string phoneNumber, DateOnly dateOfBirth, DateOnly startDate, 
                        string street, string postalCode, string city, string state, string country, Money salary)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            StartDate = startDate;
            Address = new Address(street, postalCode, city, state, country);
            Salary = new EmployeeSalary(Id, salary, DateOnly.FromDateTime(DateTime.UtcNow));
        }

        internal void UpdatePersonalInfo(string firstName, string lastName, string email, string phoneNumber, DateOnly dateOfBirth, DateOnly startDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            StartDate = startDate;
        }

        internal void UpdateAddress(string street, string postalCode, string city, string state, string country)
        {
            if (Address is null)
            {
                Address = new Address(street, postalCode, city, state, country);
            }
            else
            {
                Address.Street = street;
                Address.PostalCode = postalCode;
                Address.City = city;
                Address.State = state;
                Address.Country = country;
            }
            AddressId = Address.Id;
        }

        internal bool AssignStoreAssignment(int storeId, int jobRoleId, DateOnly from)
        {
            if (StoreAssignments.Count() >= 2)
            {
                throw new InvalidOperationException("Maximum of two active assignments reached.");
            }

            if (StoreAssignments.Any(sa => sa.StoreId == storeId))
            {
                return false;
            }

            StoreAssignments.Add(new StoreAssignment(Id, storeId, jobRoleId, from));

            return true;
        }

        internal bool ChangeEmployeeRoleInStore(int storeId, int newJobRoleId, DateOnly changeDate)
        {
            var assignment = StoreAssignments.FirstOrDefault(sa => sa.StoreId == storeId);
            if (assignment is null)
            {
                throw new KeyNotFoundException($"No active assignment for employee {Id} at store {storeId}.");
            }

            if (assignment.JobRoleId == newJobRoleId)
            {
                return false;
            }

            StoreAssignments.Remove(assignment);

            StoreAssignments.Add(new StoreAssignment(Id, storeId, newJobRoleId, changeDate));

            return true;
        }

        internal void Fire(DateOnly fireDate)
        {
            IsDeleted = true;
            EndDate = fireDate;
            StoreAssignments.Clear();
            Salary = null;
        }

        internal bool UpdateSalary(Money salary, DateOnly from)
        {
            salary.Currency = string.IsNullOrWhiteSpace(salary.Currency) ? "EUR" : salary.Currency;

            if (Salary is null)
            {
                Salary = new EmployeeSalary(Id, salary, from);
            }
            else
            {
                if (salary.Equals(Money.Create(Salary.Amount, Salary.Currency)))
                {
                    return false;
                }

                if (salary.Amount < Salary.Amount)
                {
                    throw new InvalidOperationException("New salary must be greater than the current salary.");
                }

                Salary.Amount = salary.Amount;
                Salary.Currency = salary.Currency;
                Salary.From = from;
            }

            return true;
        }
    }
}
