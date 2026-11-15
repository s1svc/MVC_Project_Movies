using CORE.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class MovieRequest : Record
    {
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(100, ErrorMessage = "{0} must be maximum {1} characters.")]
        public string Name { get; set; }

        [DisplayName("Release Date")]
        public DateTime? ReleaseDate { get; set; }

        // decimal? so that [Required] works (a plain decimal is never empty, it would be 0).
        [Required(ErrorMessage = "{0} is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} can't be negative.")]
        [DisplayName("Total Revenue")]
        public decimal? TotalRevenue { get; set; }

        // int? so that [Required] works and "-- Select --" (empty value) fails validation.
        [Required(ErrorMessage = "{0} is required.")]
        [DisplayName("Director")]
        public int? DirectorId { get; set; }

        [Required(ErrorMessage = "At least one genre is required!")]
        [DisplayName("Genres")]
        public List<int> GenreIds { get; set; }
    }
}
