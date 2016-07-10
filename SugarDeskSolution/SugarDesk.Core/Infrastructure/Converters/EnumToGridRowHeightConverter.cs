// -----------------------------------------------------------------------
// <copyright file="EnumToGridRowHeightConverter.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Core.Infrastructure.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// This class represents EnumToGridRowHeightConverter class.
    /// </summary>
    public class EnumToGridRowHeightConverter : IMultiValueConverter
    {
        /// <summary>
        /// The converting function.
        /// </summary>
        /// <param name="values">List of object values to convert.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>Converted object.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var gridOptionSouce = (EnumOptionType)values[1];
            if (values[0] == DependencyProperty.UnsetValue)
            {
                return new GridLength(0);
            }

            var gridOptionValue = (EnumOptionType)values[0];
            return (gridOptionValue == gridOptionSouce) ? new GridLength(1, GridUnitType.Star) : new GridLength(0);
        }

        /// <summary>
        /// The converting back function.
        /// </summary>
        /// <param name="value">Object to convert.</param>
        /// <param name="targetTypes">List of target types.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>List of converted objects.</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(GetType().Name + "Convert not implemented.");
        }
    }
}