// -----------------------------------------------------------------------
// <copyright file="NavigationLinksGroup.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Reporting
{
    using System.Collections.Generic;
    using Core;
    using Core.Interfaces;

    /// <summary>
    /// This class represents NavigationLinksGroup class, extends INavigationLinksGroup.
    /// </summary>
    public class NavigationLinksGroup : INavigationLinksGroup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationLinksGroup"/> class.
        /// </summary>
        public NavigationLinksGroup()
        {
            Order = 3;
            GroupDisplayName = "Reports";

            MenuLinkInfos = new List<MenuLinkInfo>();

            var assemblyName = GetType().Assembly.GetName().Name;
            MenuLinkInfos.Add(new MenuLinkInfo()
            {
                AssemblyName = assemblyName, DisplayName = "Reports", SourceRelativeUri = "/Views/ReportsView.xaml"
            });

            MenuLinkInfos.Add(new MenuLinkInfo()
            {
                AssemblyName = assemblyName, DisplayName = "About", SourceRelativeUri = "/Views/About.xaml"
            });
        }

        /// <summary>
        /// Gets or sets the order - this is the order in which the module menu item is shown.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets module menu group name.
        /// </summary>
        public string GroupDisplayName { get; set; }

        /// <summary>
        /// Gets or sets MenuLinkInfos.
        /// </summary>
        public List<MenuLinkInfo> MenuLinkInfos { get; set; }
    }
}
