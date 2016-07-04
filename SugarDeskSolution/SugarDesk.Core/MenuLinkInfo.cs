// -----------------------------------------------------------------------
// <copyright file="INavigationLinksGroup.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Core
{
    using System;

    public class MenuLinkInfo
    {
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

        public string AssemblyName { get; set; }
        public string DisplayName { get; set; }
        public string SourceRelativeUri { get; set; }
    }
}
