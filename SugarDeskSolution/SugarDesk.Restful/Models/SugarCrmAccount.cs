// -----------------------------------------------------------------------
// <copyright file="SugarCrmAccount.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Models
{
    /// <summary>
    /// This class representsSugarCRMAccount class.
    /// </summary>
    public class SugarCrmAccount
    {
        /// <summary>
        /// Gets or setsSugarCRM Rest Url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or setsSugarCRM username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or setsSugarCRM password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets a value indicating whether SugarCRMAccount object is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(Url) &&
                       !string.IsNullOrEmpty(Username) &&
                       !string.IsNullOrEmpty(Password);
            }
        }
    }
}
