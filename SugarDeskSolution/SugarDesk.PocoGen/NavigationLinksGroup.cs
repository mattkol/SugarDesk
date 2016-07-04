
using SugarDesk.Core;

namespace SugarDesk.PocoGen
{
    using System.Collections.Generic;
    using Core.Interfaces;

    public class NavigationLinksGroup : INavigationLinksGroup
    {
        public NavigationLinksGroup()
        {
            Order = 2;
            GroupDisplayName = "PocoGen";

            MenuLinkInfos = new List<MenuLinkInfo>();

            string assemblyName = GetType().Assembly.GetName().Name;
            MenuLinkInfos.Add(new MenuLinkInfo() { AssemblyName = assemblyName, DisplayName = "Poco Generator", SourceRelativeUri = "/Views/PocoGenView.xaml" });
            MenuLinkInfos.Add(new MenuLinkInfo() { AssemblyName = assemblyName, DisplayName = "About", SourceRelativeUri = "/Views/About.xaml" });
        }

        public int Order { get; set; }
        public string GroupDisplayName { get; set; }
        public List<MenuLinkInfo> MenuLinkInfos { get; set; }
    }
}

