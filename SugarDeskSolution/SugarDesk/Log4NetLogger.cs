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
    public class Log4NetLogger
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        private readonly ILog _log4NetAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetLogger"/> class.
        /// </summary>
        /// <param name="type">The logger object type.</param>
        public Log4NetLogger(Type type)
        {
            _log4NetAdapter = LogManager.GetLogger(type);
        }

        /// <summary>
        /// Debug funtion with message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Debug(string message)
        {
            _log4NetAdapter.Debug(message);
        }

        /// <summary>
        /// Debug funtion with message and excepion..
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception object to log.</param>
        public void Debug(string message, Exception exception)
        {
            _log4NetAdapter.Debug(message, exception);
        }

        /// <summary>
        /// Error funtion with message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Error(string message)
        {
            _log4NetAdapter.Error(message);
        }

        /// <summary>
        /// Error funtion with message and excepion.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception object to log.</param>
        public void Error(string message, Exception exception)
        {
            _log4NetAdapter.Error(message, exception);
        }

        /// <summary>
        /// Fatal funtion with message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Fatal(string message)
        {
            _log4NetAdapter.Fatal(message);
        }

        /// <summary>
        /// Fatal funtion with message and excepion.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception object to log.</param>
        public void Fatal(string message, Exception exception)
        {
            _log4NetAdapter.Fatal(message, exception);
        }

        /// <summary>
        /// Info funtion with message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Info(string message)
        {
            _log4NetAdapter.Info(message);
        }

        /// <summary>
        /// Info funtion with message and excepion.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception object to log.</param>
        public void Info(string message, Exception exception)
        {
            _log4NetAdapter.Info(message, exception);
        }

        /// <summary>
        /// Warning funtion with message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Warning(string message)
        {
            _log4NetAdapter.Warn(message);
        }

        /// <summary>
        /// Warning funtion with message and excepion.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception object to log.</param>
        public void Warning(string message, Exception exception)
        {
            _log4NetAdapter.Warn(message, exception);
        }
    }
}
