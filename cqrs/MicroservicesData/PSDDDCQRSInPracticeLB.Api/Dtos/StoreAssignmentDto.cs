namespace PSDDDCQRSInPracticeLB.Api.Dtos
{
    public class StoreAssignmentDto
    {
        public int? Id { get; set; }

        public int StoreId { get; set; }
        public StoreDto? Store { get; set; }

        public int JobRoleId { get; set; }
        public JobRoleDto? JobRole { get; set; }

        public DateOnly From { get; set; }
        public DateOnly? To { get; set; }
    }
}
