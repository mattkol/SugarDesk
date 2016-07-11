// -----------------------------------------------------------------------
// <copyright file="StringExtension.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Models
{
    /// <summary>
    /// This class represents StringExtension class.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// This converts command text and parameter to listbox BBCode string.
        /// </summary>
        /// <param name="itemText">Command text.</param>
        /// <param name="parameter">The command parameter.</param>
        /// <returns>The formatted string.</returns>
        public static string ListBoxBbCodeItemFormat(this string itemText,  string parameter)
        {
            if (string.IsNullOrEmpty(itemText))
            {
                return string.Empty;
            }

            return string.Format("{0}  [url=cmd://DeleteItemCommand|{1}]del[/url]", itemText, parameter);
        }
    }
}
