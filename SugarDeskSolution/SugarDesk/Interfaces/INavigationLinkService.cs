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
    
    public interface INavigationLinkService
    {
        Uri StartSoureUrl { get; }
        List<INavigationLinksGroup> NavigationLinkGroups { get; set; }
        LinkGroupCollection LinkGroupCollection { get; }
    }
}
