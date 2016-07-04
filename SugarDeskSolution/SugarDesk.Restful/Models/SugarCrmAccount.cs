using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugarDesk.Restful.Models
{
    public class SugarCrmAccount
    {
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(Url) &&
                        !string.IsNullOrEmpty(Username) &&
                        !string.IsNullOrEmpty(Password));
            }
        }
    }
}
