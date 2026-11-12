using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using CORE.Models;

namespace CORE.Services
{
    public abstract class Service
    {
        private string _culture;
        protected string Culture         {
            get
            {
                if (string.IsNullOrEmpty(_culture))
                {
                    _culture = CultureInfo.CurrentCulture.Name;
                }
                return _culture;
            }
            set
            {
                _culture = value;
                Thread.CurrentThread.CurrentCulture = new CultureInfo(_culture);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(_culture);
            }
        }

        protected Service()
        {
            Culture = "en-US";
        }

        protected CommandResponse Success(int id, string message = "Successful")
        {
            return new CommandResponse(true, message, id);
        }

        protected CommandResponse Failure(string message = "Failed")
        {
            return new CommandResponse(false, message);
        }
    }
}
