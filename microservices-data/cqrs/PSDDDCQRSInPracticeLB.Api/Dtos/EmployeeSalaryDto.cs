namespace PSDDDCQRSInPracticeLB.Api.Dtos
{
    public class EmployeeSalaryDto
    {
        public int? Id { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;

        public DateOnly From { get; set; }
        public DateOnly? To { get; set; }
    }
}
