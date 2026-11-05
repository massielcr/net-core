namespace PSDDDCQRSInPracticeLB.Api.Dtos
{
    public class EmployeeDetailsDto
    {
        public int? Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public int? AddressId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsDeleted { get; set; }
        public AddressDto? Address { get; set; }
        public EmployeeSalaryDto? Salary { get; set; }
        public ICollection<StoreAssignmentDto>? StoreAssignments { get; set; } = new List<StoreAssignmentDto>();
    }
}
