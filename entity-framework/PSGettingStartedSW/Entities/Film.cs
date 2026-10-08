using System.ComponentModel.DataAnnotations;

namespace PSGettingStartedSW.Entities
{
    public class Film
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        [Range(1900, 2099)]
        public int Year { get; set; }

        public int Length { get; set; }

        public double RatingScore { get; set; }

        public string? Mpaa { get; set; }

        public IEnumerable<Actor> Actors { get; set; } = new List<Actor>();
    }
}
