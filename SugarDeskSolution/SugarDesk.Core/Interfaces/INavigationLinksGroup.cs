// -----------------------------------------------------------------------
// <copyright file="INavigationLinksGroup.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Core.Interfaces
{
    using System.Collections.Generic;

    public interface INavigationLinksGroup
    {
        int Order { get; set; }
        string GroupDisplayName { get; set; }
        List<MenuLinkInfo> MenuLinkInfos { get; set; }
    }
}
