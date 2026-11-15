using CORE.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(60, ErrorMessage = "{0} must be at most {1} characters long.")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Retired")]
        public bool IsRetired { get; set; }
    }
}
