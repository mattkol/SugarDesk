// -----------------------------------------------------------------------
// <copyright file="MenuLinkInfo.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Core
{
    using System;

    /// <summary>
    /// This class represents MenuLinkInfo class.
    /// </summary>
    public class MenuLinkInfo
    {
        /// <summary>
        /// Gets the source Url.
        /// </summary>
        public Uri Source
        {
            get
            {
                if (string.IsNullOrEmpty(AssemblyName))
                {
                    return new Uri(SourceRelativeUri, UriKind.Relative);
                }

                return new Uri(string.Format("/{0};component{1}", AssemblyName, SourceRelativeUri), UriKind.Relative);
            }
        }

        /// <summary>
        /// Gets or sets assembly name.
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// Gets or sets menu display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets source relative Url.
        /// </summary>
        public string SourceRelativeUri { get; set; }
    }
}
