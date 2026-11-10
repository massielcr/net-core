using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSDDDCQRSInPracticeLB.Api.Entities
{
    public class StoreAssignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;

        public int StoreId { get; set; }

        public Store Store { get; set; } = null!;

        public int JobRoleId { get; set; }

        public JobRole JobRole { get; set; } = null!;

        public DateOnly From { get; set; }

        public StoreAssignment(int employeeId, int storeId, int jobRoleId, DateOnly from)
        {
            EmployeeId = employeeId;
            StoreId = storeId;
            JobRoleId = jobRoleId;
            From = from;
        }
    }
}
