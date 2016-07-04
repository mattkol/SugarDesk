// -----------------------------------------------------------------------
// <copyright file="CreateViewModel.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using CsvHelper;
using FirstFloor.ModernUI.Presentation;
using Microsoft.Win32;
using Prism.Events;
using SugarDesk.Restful.Helpers;
using SugarDesk.Restful.Models;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace SugarDesk.Restful.ViewModels
{
    using Microsoft.Practices.Unity;
    using Core.Infrastructure.Converters;

    /// <summary>
    /// This class represents CreateViewModel classs.
    /// </summary>
    public class CreateViewModel : BaseViewModel
    {
        public CreateViewModel(IEventAggregator eventAggregator, IUnityContainer container)
            : base(eventAggregator, container)
        {
            GetTemplateCommand = new RelayCommand(GetTemplate, CanGetTemplate);
            ImportCvsFileCommand = new RelayCommand(ImportCvsFile, CanImportCvsFile);
            ValidateCommand = new RelayCommand(Validate, CanValidate);
            SendCommand = new RelayCommand(Send, CanSend);
            CreateOrUpdateEntriesOption = EnumOptionType.One;
        }

        public RelayCommand GetTemplateCommand { get; private set; }
        public RelayCommand ImportCvsFileCommand { get; private set; }
        public RelayCommand ValidateCommand { get; private set; }
        public RelayCommand SendCommand { get; private set; }

        private void GetTemplate(object parameter)
        {
            List<string> properties = ModelInfoSelected.ModelProperties.Select(x => x.Name).ToList();
            string csvString = String.Join(",", properties);

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

        private bool CanGetTemplate(object parameter)
        {
            if ((ModelInfoSelected == null) || (ModelInfoSelected.ModelProperties == null))
            {
                return false;
            }

            return true;
        }

        private void ImportCvsFile(object parameter)
        {
            var dialog = new OpenFileDialog()
            {
                FileName = ModelInfoSelected.ModelName + "_" + Guid.NewGuid().ToString(),
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            DataTable dataTable = new DataTable();
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
                                ModelProperty modelProperty = ModelInfoSelected.ModelProperties.FirstOrDefault(x => (string.Compare(x.Name, header, StringComparison.CurrentCultureIgnoreCase) == 0));
                                if (modelProperty != null)
                                {
                                    dataTable.Columns.Add(modelProperty.Name, Nullable.GetUnderlyingType(modelProperty.Type) ?? modelProperty.Type);
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

        private bool CanImportCvsFile(object parameter)
        {
            if (ModelInfoSelected == null)
            {
                return false;
            }

            return true;
        }

        private void Validate(object parameter)
        {
        }

        private bool CanValidate(object parameter)
        {
            if (ModelInfoSelected == null)
            {
                return false;
            }

            return true;
        }

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

            var response = await SugarCrmApiRestful.GetAll(restRequest);

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