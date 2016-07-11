// -----------------------------------------------------------------------
// <copyright file="AddUrlContent.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Dialogs
{
    using ViewModels;
    using Views;

    /// <summary>
    /// This class represents AddUrlContent class, extends AddUrlContentView (the content view). 
    /// </summary>
    public class AddUrlContent : AddUrlContentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddUrlContent"/> class.
        /// </summary>
        /// <param name="addUrlViewModel">The model view.</param>
        public AddUrlContent(AddUrlViewModel addUrlViewModel)
        {
            DataContext = addUrlViewModel;
        }
    }
}
