using System;
namespace SugarDesk.PocoGen
{
    using System;
    using Microsoft.Practices.Unity;
    using Prism.Modularity;

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