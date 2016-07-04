using System.Collections.Generic;
using SugarDesk.Core;
using SugarDesk.Core.Interfaces;

namespace SugarDesk.Restful
{
    public class NavigationLinksGroup : INavigationLinksGroup
    {
        public NavigationLinksGroup()
        {
            Order = 1;
            GroupDisplayName = "Restful";

            MenuLinkInfos = new List<MenuLinkInfo>();

            string assemblyName = GetType().Assembly.GetName().Name;
            MenuLinkInfos.Add(new MenuLinkInfo() { AssemblyName = assemblyName, DisplayName = "Restful Crud", SourceRelativeUri = "/Views/RestfulView.xaml" });
            MenuLinkInfos.Add(new MenuLinkInfo() { AssemblyName = assemblyName, DisplayName = "About", SourceRelativeUri = "/Views/About.xaml" });
        }

        public int Order { get; set; }
        public string GroupDisplayName { get; set; }
        public List<MenuLinkInfo> MenuLinkInfos { get; set; }
    }
}
