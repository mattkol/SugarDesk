// -----------------------------------------------------------------------
// <copyright file="VisualChildFinder.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Core.Helpers
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public static class VisualChildFinder
    {
        public static T FindChild<T>(DependencyObject depObj, string childName) where T : DependencyObject
        {
            // Confirm obj is valid. 
            if (depObj == null)
            {
                return null;
            }

            // success case
            if (depObj is T && ((FrameworkElement) depObj).Name == childName)
            {
                return depObj as T;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                T obj = FindChild<T>(child, childName);

                if (obj != null)
                {
                    return obj;
                }
            }

            return null;
        }

        public static T FindChild<T>(object source, string v)
        {
            throw new NotImplementedException();
        }
    }
}
