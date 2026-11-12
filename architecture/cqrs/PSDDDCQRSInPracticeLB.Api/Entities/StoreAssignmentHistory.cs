namespace PSDDDCQRSInPracticeLB.Api.Entities
{
    public sealed class StoreAssignmentHistory
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }

        public int StoreId { get; set; }
        public int JobRoleId { get; set; }

        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
    }
}
