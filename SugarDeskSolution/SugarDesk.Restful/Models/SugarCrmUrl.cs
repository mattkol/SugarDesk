using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Biggy.Core;

namespace SugarDesk.Restful.Models
{
    public class SugarCrmUrl
    {
        [PrimaryKey(Auto: false)]
        public string Name { get; set; }
        public string Url { get; set; }

        public string Format
        {
            get
            {
                return string.Format("{0} - {1}", Name, Url);
            }
        }

        public bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(Name) &&
                        !string.IsNullOrEmpty(Url));
            }
        }
    }
}
