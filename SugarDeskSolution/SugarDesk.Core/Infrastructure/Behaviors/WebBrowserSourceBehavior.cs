// -----------------------------------------------------------------------
// <copyright file="WebBrowserSourceBehavior.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Core.Infrastructure.Behaviors
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// This class represents WebBrowserSourceBehavior class.
    /// </summary>
    public static class WebBrowserSourceBehavior
    {
        /// <summary>
        /// Create the web browser source attached property.
        /// </summary>
        public static readonly DependencyProperty WebBrowserSourceProperty =
                                                    DependencyProperty.RegisterAttached(
                                                    "WebBrowserSource",
                                                    typeof(object),
                                                    typeof(WebBrowserSourceBehavior),
                                                    new PropertyMetadata(WebBrowserSourceChanged));

        /// <summary>
        /// Set web browser source.
        /// </summary>
        /// <param name="target">DependencyObject target object</param>
        /// <param name="value">New document object to set.</param>
        public static void SetWebBrowserSource(DependencyObject target, object value)
        {
            target.SetValue(WebBrowserSourceProperty, value);
        }

        /// <summary>
        /// Get web browser source.
        /// </summary>
        /// <param name="target">DependencyObject target object</param>
        /// <returns>Web browser source object</returns>
        public static object GetWebBrowserSource(DependencyObject target)
        {
            return target.GetValue(WebBrowserSourceProperty);
        }

        /// <summary>
        /// The web browser source changed function.
        /// </summary>
        /// <param name="dependencyObject">DependencyObject object</param>
        /// <param name="eventArgs">DependencyPropertyChangedEventArgs argument</param>
        private static void WebBrowserSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var browser = dependencyObject as WebBrowser;
            if (browser != null)
            {
                var uri = eventArgs.NewValue as string;
                browser.Source = !string.IsNullOrEmpty(uri) ? new Uri(uri, UriKind.Absolute) : null;
            }
        }
    }
}
