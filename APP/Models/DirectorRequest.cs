using CORE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Models
{
    public class DirectorRequest:Record
    {
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(60, ErrorMessage = "{0} must be at most {1} characters long.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(60, ErrorMessage = "{0} must be at most {1} characters long.")]
        public string LastName { get; set; }

        public bool IsRetired { get; set; }
    }
}
