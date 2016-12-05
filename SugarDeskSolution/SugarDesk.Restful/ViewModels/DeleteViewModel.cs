// -----------------------------------------------------------------------
// <copyright file="DeleteViewModel.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.ViewModels
{
    using Core.Infrastructure.Converters;
    using FirstFloor.ModernUI.Presentation;
    using Helpers;
    using Microsoft.Practices.Unity;
    using Models;
    using Prism.Events;

    /// <summary>
    /// This class represents DeleteViewModel class.
    /// </summary>
    public class DeleteViewModel : RestfulViewModelBase
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
        private async void Send(object parameter)
        {
            ExpandPaneOption = EnumOptionType.Two;
            ResponseViewOption = EnumOptionType.One;
            EnableResponseControls = false;

            var restRequest = new RestRequest();
            restRequest.Account = CurrentSugarCrmAccount;
            restRequest.ModelInfo = ModelInfoSelected;
            restRequest.Id = Identifier;

            RestResponse response = await SugarCrmApiRestful.Delete(restRequest);

            ModuleItems = null;
            RequestJson = response.JsonRawRequest;
            ResponseJson = response.JsonRawResponse;

            ResponseViewOption = response.Failure ? EnumOptionType.Three : EnumOptionType.Two;
            EnableResponseControls = true;
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
