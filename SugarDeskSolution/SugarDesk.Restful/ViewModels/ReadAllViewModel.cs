// -----------------------------------------------------------------------
// <copyright file="ReadAllViewModel.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.ViewModels
{
    using System.Linq;
    using Core.Infrastructure.Converters;
    using FirstFloor.ModernUI.Presentation;
    using Helpers;
    using Microsoft.Practices.Unity;
    using Models;
    using Prism.Events;
    
    /// <summary>
    /// This class represents ReadAllViewModel class.
    /// </summary>
    public class ReadAllViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadAllViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="container">The Prism Unity container.</param>
        public ReadAllViewModel(IEventAggregator eventAggregator, IUnityContainer container)
            : base(eventAggregator, container)
        {
            SendCommand = new RelayCommand(Send, CanSend);
        }

        /// <summary>
        /// Gets the send command.
        /// </summary>
        public RelayCommand SendCommand { get; private set; }

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
            restRequest.Type = ModelInfoSelected.Type;
            restRequest.ModuleName = ModelInfoSelected.ModelName;
            restRequest.MaxResult = MaxResultSelected;
            restRequest.SelectFields = IsSelectFieldChecked;
            restRequest.SelectedFields = SelectedFieldsItems.ToList();

            RestResponse response = await SugarCrmApiRestful.GetAll(restRequest);

            RequestJson = response.JsonRawRequest;
            ResponseJson = response.JsonRawResponse;

            ModuleItems = response.Data;

            ResponseViewOption = EnumOptionType.Two;
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
