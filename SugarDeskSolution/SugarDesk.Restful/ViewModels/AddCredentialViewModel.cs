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

        public ObservableCollection<SugarCrmUrl> UrlItems { get; set; }
        public SugarCrmUrl UrlItemSelected { get; set; }
        public int SelectedIndexUrl { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

