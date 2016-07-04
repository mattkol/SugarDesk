namespace SugarDesk.Restful.ViewModels
{
    using Prism.Mvvm;

    public class AddUrlViewModel : BindableBase
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
