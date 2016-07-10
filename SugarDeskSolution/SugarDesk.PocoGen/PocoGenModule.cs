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
        private readonly IUnityContainer _container;

        public PocoGenModule(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        public void Initialize()
        {
        }
    }
}
