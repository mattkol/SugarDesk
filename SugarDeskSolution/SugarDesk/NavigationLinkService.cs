// -----------------------------------------------------------------------
// <copyright file="NavigationLinkService.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Interfaces;
    using FirstFloor.ModernUI.Presentation;
    using Interfaces;

    /// <summary>
    /// This class represents NavigationLinkService class, extends INavigationLinkService.
    /// </summary>
    public class NavigationLinkService : INavigationLinkService
    {
        /// <summary>
        /// The home relative url.
        /// </summary>
        private const string HomeRelUrl = "/Views/Home.xaml";

        /// <summary>
        /// The assembly name of the module.
        /// </summary>
        private readonly string _assemblyName = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationLinkService"/> class.
        /// </summary>
        public NavigationLinkService()
        {
            _assemblyName = GetType().Assembly.GetName().Name;
            NavigationLinkGroups = new List<INavigationLinksGroup>(); 
        }

        /// <summary>
        /// Gets the start source url.
        /// </summary>
        public Uri StartSoureUrl 
        { 
            get
            {
                return new Uri(string.Format("/{0};component{1}", _assemblyName, HomeRelUrl), UriKind.Relative); 
            }
        }

        /// <summary>
        /// Gets or sets the navigation link groups.
        /// </summary>
        public List<INavigationLinksGroup> NavigationLinkGroups { get; set; }

        /// <summary>
        /// Gets the menu link groups collection.
        /// </summary>
        public LinkGroupCollection LinkGroupCollection
        {
            get
            {
                if (NavigationLinkGroups == null)
                {
                    throw new Exception("No valid navigation links found.");
                }

                var menuLinkGroup = new LinkGroupCollection();
                List<INavigationLinksGroup> sortedNavigationLinksGroups = NavigationLinkGroups.OrderBy(o => o.Order).ToList();
                foreach (var navigationLinksGroup in sortedNavigationLinksGroups)
                {
                    var linkGroup = new LinkGroup
                    {
                        DisplayName = navigationLinksGroup.GroupDisplayName,
                        GroupKey = _assemblyName
                    };

                    foreach (var linkInfo in navigationLinksGroup.MenuLinkInfos)
                    {
                        if (!string.IsNullOrEmpty(linkInfo.AssemblyName) && !string.IsNullOrEmpty(linkInfo.DisplayName))
                        {
                            linkGroup.Links.Add(new Link() { DisplayName = linkInfo.DisplayName, Source = linkInfo.Source });
                        }
                    }

                    menuLinkGroup.Add(linkGroup);
                }

                return menuLinkGroup;
            }
        }
    }
}
