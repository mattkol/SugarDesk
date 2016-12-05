// -----------------------------------------------------------------------
// <copyright file="CreateViewModel.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using Core.Infrastructure.Converters;
    using CsvHelper;
    using FirstFloor.ModernUI.Presentation;
    using FirstFloor.ModernUI.Windows.Controls;
    using Microsoft.Practices.Unity;
    using Microsoft.Win32;
    using Models;
    using Prism.Events;
    using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
    using Helpers;

    /// <summary>
    /// This class represents CreateViewModel class.
    /// </summary>
    public class CreateViewModel : RestfulViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="container">The Prism Unity container.</param>
        public CreateViewModel(IEventAggregator eventAggregator, IUnityContainer container)
            : base(eventAggregator, container)
        {
            GetTemplateCommand = new RelayCommand(GetTemplate, CanGetTemplate);
            ImportCvsFileCommand = new RelayCommand(ImportCvsFile, CanImportCvsFile);
            SendCommand = new RelayCommand(Send, CanSend);
            CreateOrUpdateEntriesOption = EnumOptionType.One;
        }

        /// <summary>
        /// Gets the get template command.
        /// </summary>
        public RelayCommand GetTemplateCommand { get; private set; }

        /// <summary>
        /// Gets the import csv file command.
        /// </summary>
        public RelayCommand ImportCvsFileCommand { get; private set; }

        /// <summary>
        /// Gets the send command.
        /// </summary>
        public RelayCommand SendCommand { get; private set; }

        /// <summary>
        /// Get template function.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void GetTemplate(object parameter)
        {
            List<string> properties = ModelInfoSelected.ModelProperties.Select(x => x.Name).ToList();
            string csvString = string.Join(",", properties);

            var dialog = new SaveFileDialog()
            {
                FileName = ModelInfoSelected.ModelName + "_" + Guid.NewGuid().ToString(),
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, csvString);
            }
        }

        /// <summary>
        /// Can get template function.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>True or false.</returns>
        private bool CanGetTemplate(object parameter)
        {
            if ((ModelInfoSelected == null) || (ModelInfoSelected.ModelProperties == null))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Imports the csv from file function.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void ImportCvsFile(object parameter)
        {
            var dialog = new OpenFileDialog()
            {
                FileName = ModelInfoSelected.ModelName + "_" + Guid.NewGuid().ToString(),
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            var dataTable = new DataTable();
            if (dialog.ShowDialog() == true)
            {
                using (TextReader reader = File.OpenText(dialog.FileName))
                {
                    var csv = new CsvReader(reader);

                    bool headerIsRead = false;
                    string[] headers = null;
                    List<string> properties = ModelInfoSelected.ModelProperties.Select(x => x.Name).ToList();
                    while (csv.Read())
                    {
                        if (!headerIsRead)
                        {
                            headers = csv.FieldHeaders;
                            foreach (var header in headers)
                            {
                                ModelProperty modelProperty = ModelInfoSelected.ModelProperties.FirstOrDefault(
                                    x => (string.Compare(x.Name, header, StringComparison.CurrentCultureIgnoreCase) == 0));

                                if (modelProperty != null)
                                {
                                    dataTable.Columns.Add(
                                        modelProperty.Name, Nullable.GetUnderlyingType(modelProperty.Type) ?? modelProperty.Type);
                                }
                            }

                            headerIsRead = true;
                        }

                        var row = dataTable.NewRow();
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            row[column.ColumnName] = csv.GetField(column.DataType, column.ColumnName);
                        }

                        dataTable.Rows.Add(row);
                    }
                }
            }

            ModuleFromCsvItems = dataTable;
        }

        /// <summary>
        /// Can import the csv from file function.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>True or false.</returns>
        private bool CanImportCvsFile(object parameter)
        {
            if (ModelInfoSelected == null)
            {
                return false;
            }

            return true;
        }

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
            restRequest.Data = ModuleItems;

            RestResponse response = await SugarCrmApiRestful.Create(restRequest);

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
