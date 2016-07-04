using System.Collections.Generic;
using SugarDesk.Core;
using SugarDesk.Core.Interfaces;

namespace SugarDesk.Reporting
{
    public class NavigationLinksGroup : INavigationLinksGroup
    {
        public NavigationLinksGroup()
        {
            Order = 3;
            GroupDisplayName = "Reports";

            MenuLinkInfos = new List<MenuLinkInfo>();

            string assemblyName = GetType().Assembly.GetName().Name;
            MenuLinkInfos.Add(new MenuLinkInfo() { AssemblyName = assemblyName, DisplayName = "Reports", SourceRelativeUri = "/Views/ReportsView.xaml" });
            MenuLinkInfos.Add(new MenuLinkInfo() { AssemblyName = assemblyName, DisplayName = "About", SourceRelativeUri = "/Views/About.xaml" });
     }

        public int Order { get; set; }
        public string GroupDisplayName { get; set; }
        public List<MenuLinkInfo> MenuLinkInfos { get; set; }
    }
}

