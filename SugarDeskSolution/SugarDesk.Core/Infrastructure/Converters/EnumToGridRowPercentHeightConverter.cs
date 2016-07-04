// -----------------------------------------------------------------------
// <copyright file="EnumToGridRowPercentHeightConverter.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Core.Infrastructure.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    // this converter is only used by DatePicker to convert the font size to width and height of the icon button
    public class EnumToGridRowPercentHeightConverter : IMultiValueConverter
    {
        private const int TenPercent = 10;
        private const int NinetyPercent = 90;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var gridPercentValue = (GridPercentType)values[0];
            return (gridPercentValue == GridPercentType.Ninety) ? new GridLength(NinetyPercent, GridUnitType.Star) : new GridLength(TenPercent, GridUnitType.Star);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return (value as string).Split(' ');
        }
    }
}