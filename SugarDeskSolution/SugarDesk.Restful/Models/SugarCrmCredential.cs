using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Biggy.Core;

namespace SugarDesk.Restful.Models
{
    public class SugarCrmCredential
    {
        [PrimaryKey(Auto: false)]
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UrlName { get; set; }

        public bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(Name) &&
                        !string.IsNullOrEmpty(Username) &&
                        !string.IsNullOrEmpty(Password) &&
                        !string.IsNullOrEmpty(UrlName));
            }
        }

        public bool Equals(SugarCrmCredential credential)
        {
            if (credential == null)
            {
                return false;
            }

            if (!IsValid || !credential.IsValid)
            {
                return false;
            }

            return (string.Equals(Name, credential.Name, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(UrlName, credential.UrlName, StringComparison.OrdinalIgnoreCase));
        }

        public bool SameUrlGroup(SugarCrmCredential credential)
        {
            if (credential == null)
            {
                return false;
            }

            if (!IsValid || !credential.IsValid)
            {
                return false;
            }

            return string.Equals(UrlName, credential.UrlName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
