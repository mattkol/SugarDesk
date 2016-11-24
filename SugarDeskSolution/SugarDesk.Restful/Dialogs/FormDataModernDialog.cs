namespace SugarDesk.Restful.Dialogs
{
    using FirstFloor.ModernUI.Windows.Controls;
    using Messages;
    using Prism.Events;

    public class FormDataModernDialog : ModernDialog
    {
        /// <summary>
        /// The event aggregator
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        public FormDataModernDialog(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            this.OkButton.IsEnabled = false;
            _eventAggregator.GetEvent<EnableButtonMessage>().Subscribe(EnableButton);
        }

        /// <summary>
        /// Enable button method.
        /// </summary>
        /// <param name="enable">True or false</param>
        private void EnableButton(bool enable)
        {
            this.OkButton.IsEnabled = enable;
        }
    }
}
