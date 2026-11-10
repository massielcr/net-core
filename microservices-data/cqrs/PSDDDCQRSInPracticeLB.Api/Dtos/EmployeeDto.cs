namespace PSDDDCQRSInPracticeLB.Api.Dtos
{
    public class EmployeeDto
    {
        public int? Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public EmployeeSalaryDto? Salary { get; set; }
        public ICollection<StoreAssignmentDto>? StoreAssignments { get; set; } = new List<StoreAssignmentDto>();
    }
}
