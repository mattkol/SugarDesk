// -----------------------------------------------------------------------
// <copyright file="RestfulModule.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful
{
    using System;
    using Microsoft.Practices.Unity;
    using Prism.Modularity;

    /// <summary>
    /// This class represents RestfulModule class, extends IModule.
    /// </summary>
    public class RestfulModule : IModule
    {
        private readonly IUnityContainer _container;

        public RestfulModule(IUnityContainer container)
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
