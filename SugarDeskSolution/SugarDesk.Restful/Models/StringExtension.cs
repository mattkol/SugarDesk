using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugarDesk.Restful.Models
{
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

            charArray = Array.FindAll(charArray, (c => (char.IsLetterOrDigit(c))));
            return new string(charArray);
        }

    }
}
