namespace SugarDesk.Reporting
{
    using System;
    using Microsoft.Practices.Unity;
    using Prism.Modularity;

    public class ReportingModule : IModule
    {
        private readonly IUnityContainer _container;

        public ReportingModule(IUnityContainer container)
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