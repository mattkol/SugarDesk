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


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(GetType().Name + "Convert not implemented.");
        }
    }
}