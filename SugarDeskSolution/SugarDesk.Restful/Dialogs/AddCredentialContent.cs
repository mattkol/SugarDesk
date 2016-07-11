// -----------------------------------------------------------------------
// <copyright file="AddCredentialContent.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Dialogs
{
    using ViewModels;
    using Views;

    /// <summary>
    /// This class represents AddCredentialContent class, extends AddCredentialContentView (the content view). 
    /// </summary>
    public class AddCredentialContent : AddCredentialContentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddCredentialContent"/> class.
        /// </summary>
        /// <param name="addCredentialViewModel">The model view.</param>
        public AddCredentialContent(AddCredentialViewModel addCredentialViewModel)
        {
            DataContext = addCredentialViewModel;
        }
    }
}
