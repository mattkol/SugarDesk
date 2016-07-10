// -----------------------------------------------------------------------
// <copyright file="INavigationLinkService.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Core.Interfaces;
    using FirstFloor.ModernUI.Presentation;

    /// <summary>
    /// This interface represents INavigationLinkService class.
    /// </summary>
    public interface INavigationLinkService
    {
        /// <summary>
        /// Gets the start source url.
        /// </summary>
        Uri StartSoureUrl { get; }

        /// <summary>
        /// Gets or sets the navigation link groups.
        /// </summary>
        List<INavigationLinksGroup> NavigationLinkGroups { get; set; }

        /// <summary>
        /// Gets the menu link groups collection.
        /// </summary>
        LinkGroupCollection LinkGroupCollection { get; }
    }
}
