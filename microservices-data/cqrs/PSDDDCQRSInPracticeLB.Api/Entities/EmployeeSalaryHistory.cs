namespace PSDDDCQRSInPracticeLB.Api.Entities
{
    public sealed class EmployeeSalaryHistory
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;

        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
    }
}
