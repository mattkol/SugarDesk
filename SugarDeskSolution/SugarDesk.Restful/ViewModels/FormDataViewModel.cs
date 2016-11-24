// -----------------------------------------------------------------------
// <copyright file="FormDataViewModel.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.ViewModels
{
    using System.Collections.ObjectModel;
    using Models;
    using Prism.Mvvm;
    using System.Data;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using FirstFloor.ModernUI.Presentation;
    using FirstFloor.ModernUI.Windows.Navigation;
    using Prism.Events;
    using Messages;

    /// <summary>
    /// This class represents AddCredentialViewModel class.
    /// </summary>
    public class FormDataViewModel : BindableBase
    {
        private const string NullValue = "null";
        List<FormModuleData> emptyDataItems;
  
        /// <summary>
        /// The event aggregator
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormDataViewModel"/> class.
        /// </summary>
        /// <param name="modelName">The model name.</param>
        /// <param name="properties">The model properties.</param>
        public FormDataViewModel(IEventAggregator eventAggregator, string modelName, List<ModelProperty> properties)
        {
            _eventAggregator = eventAggregator;

            List<FormModuleData> dataItems = new List<FormModuleData>();
            emptyDataItems = new List<FormModuleData>();

            if (properties != null)
            {
                foreach (var property in properties)
                {
                    Type nullableType = Nullable.GetUnderlyingType(property.Type);
                    bool typeIsNullable = nullableType != null;
                    Type type = nullableType ?? property.Type;
                    string initValue = typeIsNullable ? NullValue : string.Empty;
                    dataItems.Add(new FormModuleData() { IsSelected = false,  FieldName = property.Name, Value = initValue, Type = property.Type, TypeName = type.Name, IsNullable = typeIsNullable });
                    emptyDataItems.Add(new FormModuleData() { IsSelected = false, FieldName = property.Name, Value = initValue, Type = property.Type, TypeName = type.Name, IsNullable = typeIsNullable });
                }
            }

            BBCodeModelInfo = string.Format("[size=16]Add data for: [b]{0}[/b] module[/size]", modelName);
            FormModuleDataItems = new ObservableCollection<FormModuleData>(dataItems);

            ClearDataCommand = new RelayCommand(ClearData);
            DataSelectedCellsChangedCommand = new RelayCommand(DataSelectedCellsChanged);

            ClearDataLinkNavigator = new DefaultLinkNavigator();
            ClearDataLinkNavigator.Commands.Add(new Uri("cmd://ClearDataCommand", UriKind.Absolute), ClearDataCommand);
        }

        /// <summary>
        /// Gets or sets the SugarCRM module data item collection.
        /// </summary>
        public ObservableCollection<FormModuleData> FormModuleDataItems { get; set; }
       
        /// <summary>
        /// Gets or sets the model info.
        /// </summary>
        public string BBCodeModelInfo { get; set; }

        
        /// <summary>
        /// Gets or sets the form data.
        /// </summary>
        public DataTable Data { get; set; }

        /// <summary>
        /// Gets the datagrid cell selection changed command.
        /// </summary>
        public RelayCommand DataSelectedCellsChangedCommand { get; private set; }

        /// <summary>
        /// Gets the export to file command.
        /// </summary>
        public RelayCommand ClearDataCommand { get; private set; }

        /// <summary>
        /// Gets the clear data link navigator command.
        /// </summary>
        public ILinkNavigator ClearDataLinkNavigator { get; private set; }


        private void DataSelectedCellsChanged(object parameter)
        {
            bool enableOkButton = false;
            if (FormModuleDataItems != null)
            {
                List<FormModuleData> list = FormModuleDataItems.Where(x => x.IsSelected ).ToList();

                if (list != null)
                {
                    enableOkButton = list.All(x => x.IsValid);
                }
            }

            _eventAggregator.GetEvent<EnableButtonMessage>().Publish(enableOkButton);
        }


        private void ClearData(object parameter)
        {
            FormModuleDataItems = new ObservableCollection<FormModuleData>(emptyDataItems);
        }

    }
}
