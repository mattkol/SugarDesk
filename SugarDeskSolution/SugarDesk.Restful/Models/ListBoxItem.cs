// -----------------------------------------------------------------------
// <copyright file="ListBoxItem.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Models
{
    using FirstFloor.ModernUI.Windows.Navigation;

    /// <summary>
    /// This class represents ListBoxItem class.
    /// </summary>
    public class ListBoxItem
    {
        /// <summary>
        /// Gets or sets the link menu name.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the BBCode.
        /// </summary>
        public string BbCode { get; set; }

        /// <summary>
        /// Gets or sets the model property.
        /// </summary>
        public ModelProperty Property { get; set; }

        /// <summary>
        /// Gets or sets the menu link navigator.
        /// </summary>
        public ILinkNavigator LinkNavigator { get; set; }
    }
}
 