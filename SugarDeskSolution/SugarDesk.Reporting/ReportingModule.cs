// -----------------------------------------------------------------------
// <copyright file="ReportingModule.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Reporting
{
    using System;
    using Microsoft.Practices.Unity;
    using Prism.Modularity;

    /// <summary>
    /// This class represents ReportingModule class, extends IModule.
    /// </summary>
    public class ReportingModule : IModule
    {
        private readonly IUnityContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingModule"/> class.
        /// </summary>
        /// <param name="container">The injected IOC container.</param>
        public ReportingModule(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        /// <summary>
        /// Initializes resources if required.
        /// </summary>
        public void Initialize()
        {
        }
    }
}
