using FirstFloor.ModernUI.Windows.Navigation;

namespace SugarDesk.Restful.Models
{
    public class ListBoxItem
    {
        public string Text { get; set;}
        public string BbCode { get; set; }
        public ModelProperty Property { get; set; }
        public ILinkNavigator LinkNavigator { get; set; }
    }
}
 