using PSGettingStartedSW.Console.Enums;

namespace PSGettingStartedSW.Console.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string ImbLink { get; set; } = string.Empty;
    }
}
