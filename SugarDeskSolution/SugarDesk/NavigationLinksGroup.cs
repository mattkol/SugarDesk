// -----------------------------------------------------------------------
// <copyright file="NavigationLinksGroup.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk
{
    using System.Collections.Generic;
    using Core;
    using Core.Interfaces;

    public class NavigationLinksGroup : INavigationLinksGroup
    {
        public NavigationLinksGroup()
        {
            Order = 0;
            GroupDisplayName = "Home";

            MenuLinkInfos = new List<MenuLinkInfo>();

            string assemblyName = GetType().Assembly.GetName().Name;
            MenuLinkInfos.Add(new MenuLinkInfo() {AssemblyName = assemblyName, DisplayName = "About", SourceRelativeUri = "/Views/Home.xaml"});
        }

        public int Order { get; set; }
        public string GroupDisplayName { get; set; }
        public List<MenuLinkInfo> MenuLinkInfos { get; set; }
    }
}


