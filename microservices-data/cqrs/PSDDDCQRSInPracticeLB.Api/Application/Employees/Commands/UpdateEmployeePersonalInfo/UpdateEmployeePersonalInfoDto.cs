namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.UpdateEmployeePersonalInfo
{
    public class UpdateEmployeePersonalInfoDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public DateOnly StartDate { get; set; }

        public UpdateEmployeeAddressDto Address { get; set; } = new();
    }
}
