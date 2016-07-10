// -----------------------------------------------------------------------
// <copyright file="AddCredentialContent.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Dialogs
{
    using ViewModels;
    using Views;

    public class AddCredentialContent : AddCredentialContentView
    {
        public AddCredentialContent(AddCredentialViewModel addCredentialViewModel)
        {
            DataContext = addCredentialViewModel;
        }
    }
}
