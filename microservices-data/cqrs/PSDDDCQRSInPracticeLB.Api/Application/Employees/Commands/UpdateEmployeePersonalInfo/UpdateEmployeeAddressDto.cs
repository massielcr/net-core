namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.UpdateEmployeePersonalInfo
{
    public class UpdateEmployeeAddressDto
    {
        public int Id { get; set; }
        public string Street { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
