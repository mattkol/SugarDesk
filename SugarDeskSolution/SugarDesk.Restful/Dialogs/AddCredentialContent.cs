namespace SugarDesk.Restful.Dialogs
{
    using ViewModels;
    using Views;

    public class AddCredentialContent : AddCredentialContentView
    {
        public AddCredentialContent(AddCredentialViewModel addCredentialViewModel)
        {
            this.DataContext = addCredentialViewModel;
        }
    }
}
