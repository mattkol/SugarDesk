// -----------------------------------------------------------------------
// <copyright file="AddUrlViewModel.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.ViewModels
{
    using Prism.Mvvm;

    /// <summary>
    /// This class represents AddUrlViewModel class.
    /// </summary>
    public class AddUrlViewModel : BindableBase
    {
        /// <summary>
        /// Gets or sets the SugarCRM Rest url name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the SugarCRM Rest url.
        /// </summary>
        public string Url { get; set; }
    }
}
