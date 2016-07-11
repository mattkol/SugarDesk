// -----------------------------------------------------------------------
// <copyright file="DeleteViewModel.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.ViewModels
{
    using FirstFloor.ModernUI.Presentation;
    using FirstFloor.ModernUI.Windows.Controls;
    using Microsoft.Practices.Unity;
    using Prism.Events;
    
    /// <summary>
    /// This class represents DeleteViewModel class.
    /// </summary>
    public class DeleteViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="container">The Prism Unity container.</param>
        public DeleteViewModel(IEventAggregator eventAggregator, IUnityContainer container)
            : base(eventAggregator, container)
        {
            SendCommand = new RelayCommand(Send, CanSend);
        }

        /// <summary>
        /// Gets the send command.
        /// </summary>
        public RelayCommand SendCommand { get; private set; }

        /// <summary>
        /// Gets or sets the reponse.
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Sends request to SugarCRM Rest API.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void Send(object parameter)
        {
            var dlg = new ModernDialog
            {
                Title = "Delete Model",
                Content = "Work in progress ..."
            };
            dlg.Buttons = new[] { dlg.OkButton, dlg.CancelButton };
            dlg.ShowDialog();
        }

        /// <summary>
        /// Can send request to SugarCRM Rest API.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>True or false.</returns>
        private bool CanSend(object parameter)
        {
            return CanSend();
        }
    }
}
