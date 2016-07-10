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
    /// This class represents ShellViewModel class.
    /// </summary>
    public class ShellViewModel : BindableBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShellViewModel"/> class.
        /// </summary>
        /// <param name="navigationLinkService">The navigarion service.</param>
        public ShellViewModel(INavigationLinkService navigationLinkService)
        {
            MenuLinkGroups = navigationLinkService.LinkGroupCollection;
            ContentSourceUrl = navigationLinkService.StartSoureUrl;
        }

        /// <summary>
        /// Gets or sets the navigation link groups.
        /// </summary>
        public LinkGroupCollection MenuLinkGroups { get; set; }

        /// <summary>
        /// Gets or sets current content source relative url.
        /// </summary>
        public Uri ContentSourceUrl { get; set; }
        
    }
}