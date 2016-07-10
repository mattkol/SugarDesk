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
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException +=
                         new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            base.OnStartup(e);

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            MessageBox.Show(exception.Message, "Uncaught Thread Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }

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
