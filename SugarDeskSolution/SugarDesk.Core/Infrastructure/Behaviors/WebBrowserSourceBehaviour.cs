// -----------------------------------------------------------------------
// <copyright file="WebBrowserSourceBehaviour.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Core.Infrastructure.Behaviors
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// This class represents WebBrowserSourceBehaviour class.
    /// </summary>
    public static class WebBrowserSourceBehaviour
    {
        public static readonly DependencyProperty WebBrowserSourceProperty =
                                                    DependencyProperty.RegisterAttached(
                                                    "WebBrowserSource",
                                                    typeof(object),
                                                    typeof(WebBrowserSourceBehaviour),
                                                    new PropertyMetadata(WebBrowserSourceChanged)
                                                    );

        /// <summary>
        /// The web browser source changed function.
        /// <param name="dependencyObject">DependencyObject object</param>
        /// <param name="eventArgs">DependencyPropertyChangedEventArgs argument</param>
        /// </summary>
        private static void WebBrowserSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var browser = dependencyObject as WebBrowser;
            if (browser != null)
            {
                var uri = eventArgs.NewValue as string;
                browser.Source = !string.IsNullOrEmpty(uri) ? new Uri(uri, UriKind.Absolute) : null;
            }
        }

        /// <summary>
        /// Set web browser source.
        /// <param name="target">DependencyObject target object</param>
        /// <param name="value">New document object to set.</param>
        /// </summary>
        public static void SetWebBrowserSource(DependencyObject target, object value)
        {
            target.SetValue(WebBrowserSourceProperty, value);
        }


        /// <summary>
        /// Get web browser source.
        /// <param name="target">DependencyObject target object</param>
        /// </summary>
        public static object GetWebBrowserSource(DependencyObject target)
        {
            return target.GetValue(WebBrowserSourceProperty);
        }

    }
}
