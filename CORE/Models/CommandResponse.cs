using CORE.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Models
{
    public class CommandResponse : Record
    {
        public bool IsSuccess { get; }
        public string Message { get;  }
        public CommandResponse( bool isSuccess, string message, int id = 0) : base(id)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public CommandResponse()
        {
        }
    }
}
