using CORE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Models
{
    public class MovieRequest : Record
    {
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(100, ErrorMessage = "{0} must be maximum {1} characters.")]
        public string Name { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public decimal TotalRevenue { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public int DirectorId { get; set; }

        [Required(ErrorMessage = "At least one genre is required!")]
        public List<int> GenreIds { get; set; } 
    
    }
}
