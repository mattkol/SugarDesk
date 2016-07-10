// -----------------------------------------------------------------------
// <copyright file="StringExtension.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Models
{
    using System;

    public static class StringExtension
    {
        public static string ListBoxBbCodeItemFormat(this string itemText,  string parameter)
        {
            if (string.IsNullOrEmpty(itemText))
            {
                return string.Empty;
            }

            return string.Format("{0}  [url=cmd://DeleteItemCommand|{1}]del[/url]", itemText, parameter);
        }

        public static string ToAlphanumericOnly(this string itemText)
        {
            if (string.IsNullOrEmpty(itemText))
            {
                return string.Empty;
            }
            char[] charArray = itemText.ToCharArray();

            charArray = Array.FindAll(charArray, c => (char.IsLetterOrDigit(c)));
            return new string(charArray);
        }

    }
}
