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

    /// <summary>
    /// This class represents Bootstrapper class.
    /// </summary>
    public class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// The folder location for modules.
        /// </summary>
        private const string ModulesFolder = @".\modules";

        /// <summary>
        /// This navigation service object.
        /// </summary>
        private INavigationLinkService _navigationLinkService = null;

        /// <summary>
        /// Creates the shell (main window).
        /// </summary>
        /// <returns>The shell dependency object.</returns>
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        /// <summary>
        /// Configure container.
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            if (_navigationLinkService != null)
            {
                // Register the link collection that is required in ShellViewModel constructor
                Container.RegisterInstance(_navigationLinkService);
            }

            RegisterTypeIfMissing(typeof(ShellViewModel), typeof(ShellViewModel), true);
        }

        /// <summary>
        /// Initialize shell.
        /// </summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Creates the module catalog.
        /// </summary>
        /// <returns>The module catalog.</returns>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = ModulesFolder };
        }

        /// <summary>
        /// Configure the module catalogs.
        /// Extracts all navigation settings for all modules.
        /// </summary>
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

        /// <summary>
        /// Process the filters.
        /// </summary>
        /// <param name="typeObj">The type of object to filter.</param>
        /// <param name="criteriaObj">Filter criteria.</param>
        /// <returns>True or false.</returns>
        private bool InterfaceFilter(Type typeObj, object criteriaObj)
        {
            return typeObj.ToString() == criteriaObj.ToString();
        }
    }
}