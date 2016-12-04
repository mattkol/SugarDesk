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
    using Helpers;

    /// <summary>
    /// This class represents AddCredentialViewModel class.
    /// </summary>
    public class FormDataViewModel : BindableBase
    {
        private List<FormModuleData> selectedModuleDataItems;
        private List<FormModuleData> emptyDataItems;
        private List<ModelProperty> properties;

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
            this.properties = properties;

            if (properties != null)
            {
                foreach (var property in properties)
                {
                    Type nullableType = Nullable.GetUnderlyingType(property.Type);
                    bool typeIsNullable = nullableType != null;
                    Type type = nullableType ?? property.Type;
                    dataItems.Add(new FormModuleData(eventAggregator) { IsSelected = false,  FieldName = property.Name, Value = string.Empty, Type = property.Type, TypeName = type.Name, IsNullable = typeIsNullable });
                    emptyDataItems.Add(new FormModuleData(eventAggregator) { IsSelected = false, FieldName = property.Name, Value = string.Empty, Type = property.Type, TypeName = type.Name, IsNullable = typeIsNullable });
                }
            }

            BBCodeModelInfo = string.Format("[size=16]Add data for: [b]{0}[/b] module[/size]", modelName);
            FormModuleDataItems = new ObservableCollection<FormModuleData>(dataItems);

            ClearDataCommand = new RelayCommand(ClearData);
            ClearDataLinkNavigator = new DefaultLinkNavigator();
            ClearDataLinkNavigator.Commands.Add(new Uri("cmd://ClearDataCommand", UriKind.Absolute), ClearDataCommand);

            _eventAggregator.GetEvent<UpdateMessage>().Subscribe(EnableButton);
        }

        /// <summary>
        /// Gets or sets the SugarCRM module data item collection.
        /// </summary>
        public ObservableCollection<FormModuleData> FormModuleDataItems { get; set; }

        public DataTable FormData
        {
            get
            {
                return (new DataTable()).FromFormData(selectedModuleDataItems);
            }
        }

        /// <summary>
        /// Gets or sets the model info.
        /// </summary>
        public string BBCodeModelInfo { get; set; }

        
        /// <summary>
        /// Gets or sets the form data.
        /// </summary>
        public DataTable Data { get; set; }

        /// <summary>
        /// Gets the export to file command.
        /// </summary>
        public RelayCommand ClearDataCommand { get; private set; }

        /// <summary>
        /// Gets the clear data link navigator command.
        /// </summary>
        public ILinkNavigator ClearDataLinkNavigator { get; private set; }

        private void EnableButton(bool update)
        {
            bool enableOkButton = false;
            if (FormModuleDataItems != null)
            {
                List<FormModuleData> list = FormModuleDataItems.Where(x => (x.IsSelected && x.IsValid)).ToList();

                if ((list != null) && (list.Count > 0))
                {
                    enableOkButton = true;
                }

                selectedModuleDataItems = list;
            }

            _eventAggregator.GetEvent<EnableButtonMessage>().Publish(enableOkButton);
        }


        private void ClearData(object parameter)
        {
            FormModuleDataItems = new ObservableCollection<FormModuleData>(emptyDataItems);
        }
    }
}
