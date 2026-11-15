using CORE.Domain;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Models
{
    public class GenreResponse : Record
    {
        public string Name { get; set; }
        [DisplayName("Movies")]
        public string MovieNamesR { get; set; }
    }
}
