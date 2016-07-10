// -----------------------------------------------------------------------
// <copyright file="SugarCrmUrl.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Models
{
    using Biggy.Core;

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
