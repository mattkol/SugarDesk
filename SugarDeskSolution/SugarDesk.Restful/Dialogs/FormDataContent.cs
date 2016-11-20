// -----------------------------------------------------------------------
// <copyright file="FormDataContent.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Dialogs
{
    using ViewModels;
    using Views;

    /// <summary>
    /// This class represents FormDataContent class, extends FormDataContentView (the content view). 
    /// </summary>
    public class FormDataContent : FormDataContentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDataContent"/> class.
        /// </summary>
        /// <param name="formDataContentViewModel">The model view.</param>
        public FormDataContent(FormDataViewModel formDataContentViewModel)
        {
            DataContext = formDataContentViewModel;
        }
    }
}
