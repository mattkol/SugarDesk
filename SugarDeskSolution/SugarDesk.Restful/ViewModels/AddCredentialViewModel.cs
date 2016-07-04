using SugarDesk.Restful.Models;

namespace SugarDesk.Restful.ViewModels
{
    using Prism.Mvvm;
    using System.Collections.ObjectModel;

    public class AddCredentialViewModel : BindableBase
    {
        public AddCredentialViewModel(ObservableCollection<SugarCrmUrl> urlItems, SugarCrmUrl urlItemSelected)
        {
            UrlItems = urlItems;
            UrlItemSelected = urlItemSelected;
            SelectedIndexUrl = 0;
        }

        public ObservableCollection<SugarCrmUrl> UrlItems { get; set; }
        public SugarCrmUrl UrlItemSelected { get; set; }
        public int SelectedIndexUrl { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

