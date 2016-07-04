// -----------------------------------------------------------------------
// <copyright file="ShellViewModel.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk
{
    using System;
    using Prism.Mvvm;
    using FirstFloor.ModernUI.Presentation;
    using Interfaces;

    /// <summary>
    /// This class represents ShellViewModel classs.
    /// </summary>
    public class ShellViewModel : BindableBase
    {

        public ShellViewModel(INavigationLinkService navigationLinkService)
        {
            MenuLinkGroups = navigationLinkService.LinkGroupCollection;
            ContentSourceUri = navigationLinkService.StartSoureUrl;
        }

        public LinkGroupCollection MenuLinkGroups { get; set; }
        public Uri ContentSourceUri { get; set; }
        
    }
}