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

    /// <summary>
    /// This class represents EnumToGridRowPercentHeightConverter class.
    /// </summary>
    public class EnumToGridRowPercentHeightConverter : IMultiValueConverter
    {
        /// <summary>
        /// Represent 10% of grid height.
        /// </summary>
        private const int TenPercent = 10;

        /// <summary>
        /// Represent 90% of grid height.
        /// </summary>
        private const int NinetyPercent = 90;

        /// <summary>
        /// The converting function.
        /// </summary>
        /// <param name="values">List of objects to convert.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>Converted object</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var gridPercentValue = (GridPercentType)values[0];
            return (gridPercentValue == GridPercentType.Ninety) ? new GridLength(NinetyPercent, GridUnitType.Star) : new GridLength(TenPercent, GridUnitType.Star);
        }

        /// <summary>
        /// The converting back function.
        /// </summary>
        /// <param name="value">Oobject to convert.</param>
        /// <param name="targetTypes">List of target types.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>List of converted objects</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(GetType().Name + "Convert not implemented.");
        }
    }
}