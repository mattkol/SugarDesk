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

    /// <summary>
    /// This class represents AddCredentialViewModel class.
    /// </summary>
    public class FormDataViewModel : BindableBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDataViewModel"/> class.
        /// </summary>
        /// <param name="urlItems">The url items.</param>
        /// <param name="urlItemSelected">The currently selected url object.</param>
        public FormDataViewModel(List<ModelProperty> properties)
        {
            List<ModuleData> dataItems = new List<ModuleData>();
            if (properties != null)
            {
                foreach (var property in properties)
                {
                    Type type = Nullable.GetUnderlyingType(property.Type) ?? property.Type;
                    dataItems.Add(new ModuleData() { FieldName = property.Name, Value = string.Empty, Type = type.Name });
                }
            }

            ModuleDataItems = new ObservableCollection<ModuleData>(dataItems);
        }

        /// <summary>
        /// Gets or sets the SugarCRM module data item collection.
        /// </summary>
        public ObservableCollection<ModuleData> ModuleDataItems { get; set; }

        /// <summary>
        /// Gets or sets the form data.
        /// </summary>
        public DataTable Data { get; set; }

    }
}
