using CORE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Models
{
    public class GenreRequest : Record
    {
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(30, ErrorMessage = "{0} must be maximum {1} characters.")]
        public string Name { get; set; }
    }
}
