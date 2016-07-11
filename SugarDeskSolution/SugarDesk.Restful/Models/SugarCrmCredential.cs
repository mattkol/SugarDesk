// -----------------------------------------------------------------------
// <copyright file="SugarCrmCredential.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Models
{
    using System;
    using Biggy.Core;

    /// <summary>
    /// This class represents SugarCrmCredential class.
    /// </summary>
    public class SugarCrmCredential
    {
        /// <summary>
        /// Gets or sets the SugarCRM credential name.
        /// </summary>
        [PrimaryKey(Auto: false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets SugarCRM Rest username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets SugarCRM Rest password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the SugarCRM Rest url associated with the credential.
        /// </summary>
        public string UrlName { get; set; }

        /// <summary>
        /// Gets a value indicating whether SugarCrmCredential object is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(Name) &&
                       !string.IsNullOrEmpty(Username) &&
                       !string.IsNullOrEmpty(Password) &&
                       !string.IsNullOrEmpty(UrlName);
            }
        }

        /// <summary>
        /// Checks if SugarCrmCredential passed object is equal to current object.
        /// </summary>
        /// <param name="credential">The SugarCrmCredential object to compare with.</param>
        /// <returns>True or false.</returns>
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

            return string.Equals(Name, credential.Name, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(UrlName, credential.UrlName, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Checks if SugarCrmCredential passed object is in the same url group with current object.
        /// </summary>
        /// <param name="credential">The SugarCrmCredential object to compare with.</param>
        /// <returns>True or false.</returns>
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
