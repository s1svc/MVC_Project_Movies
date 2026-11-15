using CORE.Domain;
using System.ComponentModel;

namespace APP.Models
{
    public class MovieResponse : Record
    {
        public string Name { get; set; }

        [DisplayName("Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [DisplayName("Total Revenue")]
        public decimal TotalRevenue { get; set; }

        public int DirectorId { get; set; }

        [DisplayName("Release Date")]
        public string ReleaseDateR { get; set; }

        [DisplayName("Total Revenue")]
        public string TotalRevenueR { get; set; }

        [DisplayName("Director")]
        public string DirectorNameR { get; set; }

        [DisplayName("Genres")]
        public string GenreNamesR { get; set; }
    }
}
