// -----------------------------------------------------------------------
// <copyright file="INavigationLinksGroup.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Core.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// This interface represents INavigationLinksGroup class.
    /// </summary>
    public interface INavigationLinksGroup
    {
        /// <summary>
        /// Gets or sets the order - this is the order in which the module menu item is shown.
        /// </summary>
        int Order { get; set; }

        /// <summary>
        /// Gets or sets module menu group name.
        /// </summary>
        string GroupDisplayName { get; set; }

        /// <summary>
        /// Gets or sets MenuLinkInfos.
        /// </summary>
        List<MenuLinkInfo> MenuLinkInfos { get; set; }
    }
}
