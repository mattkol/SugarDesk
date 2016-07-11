// -----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk
{
    using System;
    using System.Windows;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App 
    {
        /// <summary>
        /// On starup function.
        /// </summary>
        /// <param name="eventArgs">StartupEventArgs object.</param>
        protected override void OnStartup(StartupEventArgs eventArgs)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            base.OnStartup(eventArgs);

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

        /// <summary>
        /// Applicatio cuurent domain unhandled exception.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="eventArgs">UnhandledExceptionEventArgs object.</param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs eventArgs)
        {
            var exception = eventArgs.ExceptionObject as Exception;
            MessageBox.Show(exception.Message, "Uncaught Thread Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Applicatio dispatcher unhandled exception.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="eventArgs">DispatcherUnhandledExceptionEventArgs object.</param>
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs eventArgs)
        {
            string message = "An error as occured!";
            var exception = new Exception(message);

            if (eventArgs.Exception != null)
            {
                exception = eventArgs.Exception.GetBaseException();
                if (!string.IsNullOrEmpty(exception.Message))
                {
                    message = exception.Message;
                }
            }

            MessageBox.Show(message, "Application Error");
            eventArgs.Handled = true;

            var logger = new Log4NetLogger(typeof(App));
            logger.Error(message, exception);
        }
    }
}
