namespace PSGettingStartedSW.Entity
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Length { get; set; }
        public double RatingScore { get; set; }
        public string? Mpaa { get; set; }
    }
}
