// -----------------------------------------------------------------------
// <copyright file="ReadByIdViewModel.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using FirstFloor.ModernUI.Presentation;
using Prism.Events;
using SugarDesk.Restful.Helpers;
using SugarDesk.Restful.Models;

namespace SugarDesk.Restful.ViewModels
{
    using Microsoft.Practices.Unity;
    using Core.Infrastructure.Converters;

    /// <summary>
    /// This class represents ReadByIdViewModel classs.
    /// </summary>
    public class ReadByIdViewModel : BaseViewModel
    {
        public ReadByIdViewModel(IEventAggregator eventAggregator, IUnityContainer container)
            : base(eventAggregator, container)
        {
            SendCommand = new RelayCommand(Send, CanSend);
        }

        public RelayCommand SendCommand { get; private set; }
        
        private async void Send(object parameter)
        {
            ExpandPaneOption = EnumOptionType.Two;
            ResponseViewOption = EnumOptionType.One;
            EnableResponseControls = false;

            var restRequest = new RestRequest();
            restRequest.Account = CurrentSugarCrmAccount;
            restRequest.Type = ModelInfoSelected.Type;
            restRequest.ModuleName = ModelInfoSelected.ModelName;
            restRequest.Id = Identifier;
            restRequest.SelectFields = IsSelectFieldChecked;
            restRequest.SelectedFields = SelectedFieldsItems.ToList();

            var response = await SugarCrmApiRestful.GetById(restRequest);

            RequestJson = response.JsonRawRequest;
            ResponseJson = response.JsonRawResponse;

            ModuleItems = response.Data;

            ResponseViewOption = EnumOptionType.Two;
            EnableResponseControls = true;
        }

        private bool CanSend(object parameter)
        {
            if (ModelInfoSelected == null)
            {
                return false;
            }

            if (IsSelectFieldChecked)
            {
                if ((SelectedFieldsItems == null) || (SelectedFieldsItems.Count <= 0))
                {
                    return false;
                }
            }

            return true;
        }
    }
}