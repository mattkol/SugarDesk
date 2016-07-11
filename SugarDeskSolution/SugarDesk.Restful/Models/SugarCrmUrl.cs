// -----------------------------------------------------------------------
// <copyright file="SugarCrmUrl.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Models
{
    using Biggy.Core;

    /// <summary>
    /// This class represents SugarCrmUrl class.
    /// </summary>
    public class SugarCrmUrl
    {
        /// <summary>
        /// Gets or sets the SugarCRM url name.
        /// </summary>
        [PrimaryKey(Auto: false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the SugarCRM url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets the formatted SugarCRM name and url.
        /// </summary>
        public string Format
        {
            get
            {
                return string.Format("{0} - {1}", Name, Url);
            }
        }
        
        /// <summary>
        /// Gets a value indicating whether SugarCrmUrl object is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(Name) &&
                       !string.IsNullOrEmpty(Url);
            }
        }
    }
}
