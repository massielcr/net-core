using PSDDDCQRSInPracticeLB.Api.Entities.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSDDDCQRSInPracticeLB.Api.Entities
{
    public class EmployeeSalary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;

        public decimal Amount { get; set; }

        public string? Currency { get; set; } = string.Empty;

        public DateOnly From { get; set; }

        public EmployeeSalary(int employeeId, Money value, DateOnly from)
        {
            EmployeeId = employeeId;
            Amount = value.Amount;
            Currency = value.Currency;
            From = from;
        }
    }
}
