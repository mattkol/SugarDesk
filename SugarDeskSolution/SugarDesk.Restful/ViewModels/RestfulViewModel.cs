// -----------------------------------------------------------------------
// <copyright file="RestViewModel.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------


using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Prism.Events;
using SugarCrm.RestApiCalls;
using SugarCrm.RestApiCalls.Models;
using SugarDesk.Restful.Dialogs;
using SugarDesk.Restful.Messages;
using SugarDesk.Restful.Models;

namespace SugarDesk.Restful.ViewModels
{
    using System;
    using Core.Infrastructure.Converters;
    using FirstFloor.ModernUI.Presentation;
    using FirstFloor.ModernUI.Windows.Controls;
    using Prism.Mvvm;

    /// <summary>
    /// This class represents RestfulViewModel classs.
    /// </summary>
    public class RestfulViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IUnityContainer _container;
        private readonly SugarCrmAccount _currentSugarCrmAccount;

        public RestfulViewModel(IEventAggregator eventaggregator, IUnityContainer container)
        {
            _eventAggregator = eventaggregator;
            _container = container;
            _currentSugarCrmAccount = new SugarCrmAccount();
            Application.Current.Properties["SugarCrmAccount"] = _currentSugarCrmAccount;

            UrlSelectionChangedCommand = new RelayCommand(UrlSelectionChanged);
            CredentialSelectionChangedCommand = new RelayCommand(CredentialSelectionChanged);
            DeleteUrlCommand = new RelayCommand(DeleteUrl, CanDeleteUrl);
            AddUrlCommand = new RelayCommand(AddUrl);
            DeleteCredentialCommand = new RelayCommand(DeleteCredential, CanDeleteCredential);
            AddCredentialCommand = new RelayCommand(AddCredential);

            GridRowType = EnumOptionType.Two;
            GetModuleInfoList();
            LoadDefaultSugarCrmAccounts();
        }

        public RelayCommand UrlSelectionChangedCommand { get; private set; }
        public RelayCommand CredentialSelectionChangedCommand { get; private set; }
        public RelayCommand DeleteUrlCommand { get; private set; }
        public RelayCommand AddUrlCommand { get; private set; }
        public RelayCommand DeleteCredentialCommand { get; private set; }
        public RelayCommand AddCredentialCommand { get; private set; }

        public EnumOptionType GridRowType { get; set; }
        public ObservableCollection<ModelInfo> ModelInfoItems { get; set; }
        public ObservableCollection<SugarCrmUrl> UrlItems { get; set; }
        public ObservableCollection<SugarCrmCredential> CredentialItems { get; set; }

        public SugarCrmUrl UrlItemSelected { get; set; }
        public SugarCrmCredential CredentialItemSelected { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }


        private void UrlSelectionChanged(object parameter)
        {
            var selectedUrl = UrlItemSelected;
            if (selectedUrl != null && selectedUrl.IsValid)
            {
                UpdateUrl(UrlItemSelected);
                var sugarCrmCredentials = SugarCrmAccountService.Instance.GetCredntialList(UrlItemSelected.Name);

                if (sugarCrmCredentials != null)
                {
                    CredentialItems = new ObservableCollection<SugarCrmCredential>(sugarCrmCredentials);
                }

                CredentialItemSelected = null;
                Username = string.Empty;
                Password = string.Empty;

                if ((sugarCrmCredentials != null) && (sugarCrmCredentials.Count > 0))
                {
                    CredentialItemSelected = sugarCrmCredentials[0];
                    Username = CredentialItemSelected.Username;
                    Password = CredentialItemSelected.Password;
                    UpdateCredntial(CredentialItemSelected);
                }
            }
        }

        private void CredentialSelectionChanged(object parameter)
        {
            var credentialSelected = CredentialItemSelected;
            if (credentialSelected != null && credentialSelected.IsValid)
            {
                Username = credentialSelected.Username;
                Password = credentialSelected.Password;
                UpdateCredntial(credentialSelected);
            }
        }

        private void DeleteUrl(object parameter)
        {
            if (UrlItemSelected != null)
            {
                if (SugarCrmAccountService.Instance.RemoveUrl(UrlItemSelected.Name))
                {
                    var sugarCrmList = SugarCrmAccountService.Instance.UrlList;

                    if (sugarCrmList != null)
                    {
                        UrlItems = new ObservableCollection<SugarCrmUrl>(sugarCrmList);
                        UrlItemSelected = sugarCrmList[0];
                        UpdateUrl(UrlItemSelected);
                        var sugarCrmCredentials = SugarCrmAccountService.Instance.GetCredntialList(UrlItemSelected.Name);

                        if (sugarCrmCredentials != null)
                        {
                            CredentialItems = new ObservableCollection<SugarCrmCredential>(sugarCrmCredentials);
                        }

                        if ((sugarCrmCredentials != null) && (sugarCrmCredentials.Count > 0))
                        {
                            CredentialItemSelected = sugarCrmCredentials[0];
                            Username = CredentialItemSelected.Username;
                            Password = CredentialItemSelected.Password;
                            UpdateCredntial(CredentialItemSelected);
                        }
                    } 
                }
            }
        }

        private bool CanDeleteUrl(object parameter)
        {
            if (UrlItemSelected != null)
            {
                return true;
            }

            return false;
        }

        private void AddUrl(object parameter)
        {
            var addUrlViewModel = new AddUrlViewModel();

            var dlg = new ModernDialog
            {
                Title = "Add Url",
                Content = new AddUrlContent(addUrlViewModel)
            };
            dlg.Buttons = new[] { dlg.OkButton, dlg.CancelButton };
            dlg.ShowDialog();

            if (dlg.MessageBoxResult == MessageBoxResult.OK)
            {
                var name = addUrlViewModel.Name;
                var url = addUrlViewModel.Url;

                var sugarCrmUrl = new SugarCrmUrl {Name = name, Url = url};

                if (sugarCrmUrl.IsValid)
                {
                    SugarCrmAccountService.Instance.AddUrl(sugarCrmUrl);
                    var sugarCrmList = SugarCrmAccountService.Instance.UrlList;

                    if (sugarCrmList != null)
                    {
                        UrlItems = new ObservableCollection<SugarCrmUrl>(sugarCrmList);
                    } 
                }
            }
        }

        private void DeleteCredential(object parameter)
        {
            if (CredentialItemSelected != null)
            {
                if (SugarCrmAccountService.Instance.RemoveCredntial(CredentialItemSelected))
                {
                    var sugarCrmCredentials = SugarCrmAccountService.Instance.GetCredntialList(UrlItemSelected.Name);

                    if (sugarCrmCredentials != null)
                    {
                        CredentialItems = new ObservableCollection<SugarCrmCredential>(sugarCrmCredentials);
                    }

                    if ((sugarCrmCredentials != null) && (sugarCrmCredentials.Count > 0))
                    {
                        CredentialItemSelected = sugarCrmCredentials[0];
                        Username = CredentialItemSelected.Username;
                        Password = CredentialItemSelected.Password;
                        UpdateCredntial(CredentialItemSelected);
                    }
                }
            }
        }

        private bool CanDeleteCredential(object parameter)
        {
            if (CredentialItemSelected != null)
            {
                return true;
            }

            return false;
        }

        private void AddCredential(object parameter)
        {
            var addCredentialViewModel = new AddCredentialViewModel(UrlItems, UrlItemSelected);

            var dlg = new ModernDialog
            {
                Title = "Add Credential",
                Content = new AddCredentialContent(addCredentialViewModel)
            };

            dlg.Buttons = new[] { dlg.OkButton, dlg.CancelButton };
            dlg.ShowDialog();

            if (dlg.MessageBoxResult == MessageBoxResult.OK)
            {
                var name = addCredentialViewModel.Name;
                var username = addCredentialViewModel.Username;
                var password = addCredentialViewModel.Password;

                var sugarCrmCredential = new SugarCrmCredential { Name = name, Username = username, Password = password };

                if (sugarCrmCredential.IsValid)
                {
                    SugarCrmAccountService.Instance.AddCredntial(sugarCrmCredential);
                }

                if ((CredentialItemSelected == null) || !CredentialItemSelected.SameUrlGroup(sugarCrmCredential))
                {
                    CredentialItemSelected = sugarCrmCredential;
                    Username = CredentialItemSelected.Username;
                    Password = CredentialItemSelected.Password;
                    UpdateCredntial(CredentialItemSelected);
                }
            }
        }

        private void GetModuleInfoList()
        {
            var modelInfos = new List<ModelInfo>();

            var types = from type in typeof(ModulePropertyAttribute).Assembly.GetTypes()
                        where Attribute.IsDefined(type, typeof(ModulePropertyAttribute))
                        select type;

            foreach (var type in types)
            {
                object[] classAttrs = type.GetCustomAttributes(typeof(ModulePropertyAttribute), false);
                if (classAttrs.Length == 1)
                {
                    string modelName = ((ModulePropertyAttribute)classAttrs[0]).ModuleName;
                    var modelInfo = new ModelInfo();
                    modelInfo.ModelName = modelName;
                    modelInfo.Type = type;
                    modelInfo.ModelProperties = new List<ModelProperty>();

                    var props = type.GetProperties();
                    foreach (PropertyInfo prop in props)
                    {
                        object[] propAttrs = prop.GetCustomAttributes(true);
                        foreach (object attr in propAttrs)
                        {
                            var modelProperty = new ModelProperty();
                            var jsonProperty = attr as JsonPropertyAttribute;
                            if (jsonProperty != null)
                            {
                                modelProperty.Name = prop.Name;
                                modelProperty.Type = prop.PropertyType;
                                modelProperty.JsonName = jsonProperty.PropertyName;
                                modelInfo.ModelProperties.Add(modelProperty);
                            }
                        }
                    }

                    modelInfos.Add(modelInfo);
                }
            }

            // Register the link collection that is required in ShellViewModel constructor
            _container.RegisterInstance(modelInfos);

            ModelInfoItems = new ObservableCollection<ModelInfo>(modelInfos);
        }

        private void UpdateUrl(SugarCrmUrl sugarCrmUrl)
        {
            _currentSugarCrmAccount.Url = string.Empty;
            if ((sugarCrmUrl != null) && sugarCrmUrl.IsValid)
            {
                _currentSugarCrmAccount.Url = sugarCrmUrl.Url;
            }

            _eventAggregator.GetEvent<AccountMessage>().Publish(_currentSugarCrmAccount);
        }

        private void UpdateCredntial(SugarCrmCredential sugarCrmCredential)
        {
            _currentSugarCrmAccount.Username = string.Empty;
            _currentSugarCrmAccount.Password = string.Empty;
            if ((sugarCrmCredential != null) && sugarCrmCredential.IsValid)
            {
                _currentSugarCrmAccount.Username = sugarCrmCredential.Username;
                _currentSugarCrmAccount.Password = sugarCrmCredential.Password;
              }

            _eventAggregator.GetEvent<AccountMessage>().Publish(_currentSugarCrmAccount);
        }

        private void LoadDefaultSugarCrmAccounts()
        {
            UrlItems = new ObservableCollection<SugarCrmUrl>();
            CredentialItems = new ObservableCollection<SugarCrmCredential>();

            var sugarCrmUrls = SugarCrmAccountService.Instance.UrlList;

            if (sugarCrmUrls != null)
            {
                UrlItems = new ObservableCollection<SugarCrmUrl>(sugarCrmUrls);
            }

            if ((sugarCrmUrls != null) && (sugarCrmUrls.Count > 0))
            {
                UrlItemSelected = sugarCrmUrls[0];
                _currentSugarCrmAccount.Url = UrlItemSelected.Url;
                var sugarCrmCredentials = SugarCrmAccountService.Instance.GetCredntialList(UrlItemSelected.Name);

                if (sugarCrmCredentials != null)
                {
                    CredentialItems = new ObservableCollection<SugarCrmCredential>(sugarCrmCredentials);
                } 

                if ((sugarCrmCredentials != null) && (sugarCrmCredentials.Count > 0))
                {
                    CredentialItemSelected = sugarCrmCredentials[0];
                    Username = CredentialItemSelected.Username;
                    Password = CredentialItemSelected.Password;
                    _currentSugarCrmAccount.Username = CredentialItemSelected.Username;
                    _currentSugarCrmAccount.Password = CredentialItemSelected.Password;
                }

                Application.Current.Properties["SugarCrmAccount"] = _currentSugarCrmAccount;
            }
        }
    }
}