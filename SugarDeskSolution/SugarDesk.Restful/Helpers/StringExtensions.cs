// -----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Helpers
{
    using System.IO;

    /// <summary>
    /// This class represents StringExtensions class.
    /// </summary>
    public static class StringExtensions
    {
        public static bool FileIsValid(this string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }
            else
            {
                if (new FileInfo(filePath).Length == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }

}
