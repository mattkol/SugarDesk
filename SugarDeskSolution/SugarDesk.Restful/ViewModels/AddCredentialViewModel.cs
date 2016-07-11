// -----------------------------------------------------------------------
// <copyright file="AddCredentialViewModel.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.ViewModels
{
    using System.Collections.ObjectModel;
    using Models;
    using Prism.Mvvm;

    /// <summary>
    /// This class represents AddCredentialViewModel class.
    /// </summary>
    public class AddCredentialViewModel : BindableBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddCredentialViewModel"/> class.
        /// </summary>
        /// <param name="urlItems">The url items.</param>
        /// <param name="urlItemSelected">The currently selected url object.</param>
        public AddCredentialViewModel(ObservableCollection<SugarCrmUrl> urlItems, SugarCrmUrl urlItemSelected)
        {
            UrlItems = urlItems;
            UrlItemSelected = urlItemSelected;
            SelectedIndexUrl = 0;
        }

        /// <summary>
        /// Gets or sets the SugarCrmUrl object collection.
        /// </summary>
        public ObservableCollection<SugarCrmUrl> UrlItems { get; set; }

        /// <summary>
        /// Gets or sets the currently selected SugarCrmUrl object.
        /// </summary>
        public SugarCrmUrl UrlItemSelected { get; set; }

        /// <summary>
        /// Gets or sets the currently selected url collection index.
        /// </summary>
        public int SelectedIndexUrl { get; set; }

        /// <summary>
        /// Gets or sets the SugarCRM Rest url name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the SugarCRM REST username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the SugarCRM REST password.
        /// </summary>
        public string Password { get; set; }
    }
}
