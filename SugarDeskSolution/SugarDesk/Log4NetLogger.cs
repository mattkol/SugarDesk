// -----------------------------------------------------------------------
// <copyright file="Log4NetLogger.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk
{
    using System;
    using log4net;

    /// <summary>
    /// This class represents Log4NetLogger class.
    /// </summary>
    internal class Log4NetLogger
    {
        private readonly ILog _log4NetAdapter;

        public Log4NetLogger(Type type)
        {
            this._log4NetAdapter = LogManager.GetLogger(type);
        }

        public void Debug(string message)
        {
            this._log4NetAdapter.Debug(message);
        }

        public void Debug(string message, Exception exception)
        {
            this._log4NetAdapter.Debug(message, exception);
        }

        public void Error(string message)
        {
            this._log4NetAdapter.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            this._log4NetAdapter.Error(message, exception);
        }

        public void Fatal(string message)
        {
            this._log4NetAdapter.Fatal(message);
        }

        public void Fatal(string message, Exception exception)
        {
            this._log4NetAdapter.Fatal(message, exception);
        }

        public void Info(string message)
        {
            this._log4NetAdapter.Info(message);
        }

        public void Info(string message, Exception exception)
        {
            this._log4NetAdapter.Info(message, exception);
        }

        public void Warning(string message)
        {
            this._log4NetAdapter.Warn(message);
        }

        public void Warning(string message, Exception exception)
        {
            this._log4NetAdapter.Warn(message, exception);
        }
    }
}
