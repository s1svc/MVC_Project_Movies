using APP.Domain;
using CORE.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Models
{
    public class DirectorResponse : Record
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isRetired { get; set; }
        public List<Movie> Movies { get; set; }

        public string FullNameR { get; set; }
        public string isRetiredR { get; set; }
        public List<string> MovieNamesR { get; set; }
    }
}
