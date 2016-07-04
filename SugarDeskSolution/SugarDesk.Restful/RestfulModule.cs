namespace SugarDesk.Restful
{
    using System;
    using Microsoft.Practices.Unity;
    using Prism.Modularity;

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