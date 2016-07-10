// -----------------------------------------------------------------------
// <copyright file="AddUrlContent.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Dialogs
{
    using ViewModels;
    using Views;

    public class AddUrlContent : AddUrlContentView
    {
        public AddUrlContent(AddUrlViewModel addUrlViewModel)
        {
            DataContext = addUrlViewModel;
        }
    }
}
