// -----------------------------------------------------------------------
// <copyright file="PocoGenModule.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.PocoGen
{
    using System;
    using Microsoft.Practices.Unity;
    using Prism.Modularity;

    /// <summary>
    /// This class represents PocoGenModule class, extends IModule.
    /// </summary>
    public class PocoGenModule : IModule
    {
        /// <summary>
        /// The injected IOC container.
        /// </summary>
        private readonly IUnityContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="PocoGenModule"/> class.
        /// </summary>
        /// <param name="container">The injected IOC container.</param>
        public PocoGenModule(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        /// <summary>
        /// Initializes a module.
        /// </summary>
        public void Initialize()
        {
        }
    }
}
