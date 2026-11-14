
using CORE.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Models
{
    public class MovieResponse : Record
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<int> GenreIds { get; set; }
        public int DirectorId { get; set; }
        
        public string RelaseDateR { get; set; }
        public string DirectorR { get; set; }
        public string GenreNamesR { get; set; }
    }
}
