using CORE.Domain;
using System.ComponentModel;

namespace APP.Models
{
    public class DirectorResponse : Record
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Retired")]
        public bool IsRetired { get; set; }

        [DisplayName("Director")]
        public string FullNameR { get; set; }

        [DisplayName("Retired")]
        public string IsRetiredR { get; set; }

        [DisplayName("Movies")]
        public string MovieNamesR { get; set; }
    }
}
