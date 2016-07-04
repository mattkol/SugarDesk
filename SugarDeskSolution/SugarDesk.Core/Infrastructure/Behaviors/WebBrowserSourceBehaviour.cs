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

    public static class WebBrowserSourceBehaviour
    {
        public static readonly DependencyProperty WebBrowserSourceProperty =
            DependencyProperty.RegisterAttached(
            "WebBrowserSource",
            typeof(object),
            typeof(WebBrowserSourceBehaviour),
            new PropertyMetadata(WebBrowserSourceChanged)
            );

        private static void WebBrowserSourceChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var browser = d as WebBrowser;
            if (browser != null)
            {
                var uri = e.NewValue as string;
                browser.Source = !string.IsNullOrEmpty(uri) ? new Uri(uri, UriKind.Absolute) : null;
            }
        }

        public static void SetWebBrowserSource(DependencyObject target, object value)
        {
            target.SetValue(WebBrowserSourceProperty, value);
        }

        public static object GetWebBrowserSource(DependencyObject target)
        {
            return target.GetValue(WebBrowserSourceProperty);
        }

    }
}
