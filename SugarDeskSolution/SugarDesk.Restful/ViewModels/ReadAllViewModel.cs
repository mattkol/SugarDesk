// -----------------------------------------------------------------------
// <copyright file="ReadAllViewModel.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using FirstFloor.ModernUI.Presentation;
using Prism.Events;
using SugarDesk.Restful.Helpers;
using SugarDesk.Restful.Models;

namespace SugarDesk.Restful.ViewModels
{
    using Microsoft.Practices.Unity;
    using Core.Infrastructure.Converters;

    /// <summary>
    /// This class represents RestViewModel classs.
    /// </summary>
    public class ReadAllViewModel : BaseViewModel
    {
        public ReadAllViewModel(IEventAggregator eventAggregator, IUnityContainer container)
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

        private bool CanSend(object parameter)
        {
            return CanSend();
        }

    }
}