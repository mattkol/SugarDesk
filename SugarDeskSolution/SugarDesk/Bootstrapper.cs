// -----------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk
{
    using System;
    using System.Reflection;
    using System.Windows;
    using Core.Interfaces;
    using Interfaces;
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

    class Bootstrapper : UnityBootstrapper
    {
        private const string ModulesFolder = @".\modules";
        private INavigationLinkService _navigationLinkService = null;
 
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            if (_navigationLinkService != null)
            {
                // Register the link collection that is required in ShellViewModel constructor
                Container.RegisterInstance(_navigationLinkService);
            }
            this.RegisterTypeIfMissing(typeof(ShellViewModel), typeof(ShellViewModel), true);
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = ModulesFolder };
        }

        protected override void ConfigureModuleCatalog()
        {
            // Dynamic Modules are copied to a directory as part of a post-build step.
            // These modules are not referenced in the startup project and are discovered 
            // by examining the assemblies in a directory. The module projects have the 
            // following post-build step in order to copy themselves into that directory:
            //
            // xcopy "$(TargetDir)$(TargetFileName)" "$(TargetDir)modules\" /y
            //
            // WARNING! Do not forget to explicitly compile the solution before each running
            // so the modules are copied into the modules folder.
            var directoryCatalog = (DirectoryModuleCatalog)ModuleCatalog;
            directoryCatalog.Initialize();

            _navigationLinkService = new NavigationLinkService();
            var typeFilter = new TypeFilter(InterfaceFilter);

            // First - add executing module naviagtion 
            _navigationLinkService.NavigationLinkGroups.Add(new NavigationLinksGroup());

            foreach (var module in directoryCatalog.Items)
            {
                var moduleInfo = (ModuleInfo)module;
                var assembly = Assembly.LoadFrom(moduleInfo.Ref);

                foreach (Type t in assembly.GetTypes())
                {
                    var navigationInterfaces = t.FindInterfaces(typeFilter, typeof(INavigationLinksGroup).ToString());

                    if (navigationInterfaces.Length > 0)
                    {
                        // Get all links (menus) for a module
                        var navigationLinksGroup = (INavigationLinksGroup)assembly.CreateInstance(t.FullName);
                        _navigationLinkService.NavigationLinkGroups.Add(navigationLinksGroup);
                    }
                }
            }

            // Use this to add local module if available
            //  var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            //  moduleCatalog.AddModule(typeof(Local Module ));
        }

        private bool InterfaceFilter(Type typeObj, Object criteriaObj)
        {
            return typeObj.ToString() == criteriaObj.ToString();
        }
    }
}