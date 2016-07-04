namespace SugarDesk.Restful.Dialogs
{
    using ViewModels;
    using Views;

    public class AddUrlContent : AddUrlContentView
    {
        public AddUrlContent(AddUrlViewModel addUrlViewModel)
        {
            this.DataContext = addUrlViewModel;
        }
    }
}
