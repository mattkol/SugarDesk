// -----------------------------------------------------------------------
// <copyright file="ReadAllViewModel.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Linq;
    using System.Windows;
    using FirstFloor.ModernUI.Presentation;
    using FirstFloor.ModernUI.Windows.Navigation;
    using Prism.Events;
    using Prism.Mvvm;
    using Messages;
    using Models;
    using Microsoft.Practices.Unity;
    using Core.Infrastructure.Converters;

    /// <summary>
    /// This class represents RestViewModel classs.
    /// </summary>
    public abstract class BaseViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IUnityContainer _container;
        private EnumOptionType _expandPaneOption;
        private List<ModelInfo> _modelInfos;
        private List<ModuleData> _moduleDataItems;

        protected BaseViewModel(IEventAggregator eventaggregator, IUnityContainer container)
        {
            _eventAggregator = eventaggregator;
            _container = container;

            _eventAggregator.GetEvent<AccountMessage>().Subscribe(UpdateCurrentAccount);

            object sugarCrmAccountObj = Application.Current.Properties["SugarCrmAccount"];
            if (sugarCrmAccountObj != null)
            {
                CurrentSugarCrmAccount = (SugarCrmAccount) sugarCrmAccountObj;
            }

            RequestViewOption = EnumOptionType.One;
            ResponseViewOption = EnumOptionType.One;

            AddFieldItemCommand = new RelayCommand(AddFieldItem);
            AddFieldItem2Command = new RelayCommand(AddFieldItem2);
            DeleteFieldItemCommand = new RelayCommand(DeleteFieldItem);
            DeleteFieldItem2Command = new RelayCommand(DeleteFieldItem2);
            ModuleSelectionChangedCommand = new RelayCommand(ModuleSelectionChanged);

            ExpandPaneOption = EnumOptionType.One;
            IsSelectFieldChecked = false;
            EnableSingleDataEntryControls = false;
            ShowSelectFieldDataGrid = false;

            SelectedFieldsItems = new ObservableCollection<ListBoxItem>();
            InitControls();
        }

        protected SugarCrmAccount CurrentSugarCrmAccount { get; private set; }
        public RelayCommand AddFieldItemCommand { get; private set; }
        public RelayCommand AddFieldItem2Command { get; private set; }
        public RelayCommand DeleteFieldItemCommand { get; private set; }
        public RelayCommand DeleteFieldItem2Command { get; private set; }
        public RelayCommand ModuleSelectionChangedCommand { get; private set; }
        
        public ObservableCollection<ModelInfo> ModelInfoItems { get; set; }
        public ObservableCollection<ModuleData> ModuleDataItems { get; set; }
        public DataTable ModuleFromCsvItems { get; set; }
        public ObservableCollection<ModelProperty> FieldItems { get; set; }
        public ObservableCollection<int> MaxResultItems { get; set; }
        public ObservableCollection<ListBoxItem> SelectedFieldsItems { get; set; }
        public DataTable ModuleItems { get; set; }
        
        public ModelInfo ModelInfoSelected { get; set; }
        public ModelProperty SelectedFieldItem { get; set; }
        public ModuleData SelectedModuleDataItem { get; set; }
        
        public ListBoxItem FieldItemSelected { get; set; }
        public string Identifier { get; set; }
        public int PageNumber { get; set; }
        public int MaxResultSelected { get; set; }
        public int SelectedIndeMaxResult { get; set; }
        
        public EnumOptionType RequestViewOption { get; set; }
        public EnumOptionType ResponseViewOption { get; set; }
        public EnumOptionType CreateOrUpdateEntriesOption  { get; set; }
        public GridPercentType RequestRowGridPercent { get; set; }
        public GridPercentType ResponseRowGridPercent { get; set; }

        public string RequestJson { get; set; }
        public string ResponseJson { get; set; }

        public bool IsSelectFieldChecked { get; set; }
        public bool EnableResponseControls { get; set; }
        public bool EnableSingleDataEntryControls { get; set; } 
        public bool ShowSelectFieldDataGrid { get; set; }

        public EnumOptionType ExpandPaneOption
        {
            get { return _expandPaneOption; }
            set
            {
                if (value == EnumOptionType.One)
                {
                    RequestRowGridPercent = GridPercentType.Ninety;
                    ResponseRowGridPercent = GridPercentType.Ten;
                }
                else
                {
                    RequestRowGridPercent = GridPercentType.Ten;
                    ResponseRowGridPercent = GridPercentType.Ninety;
                }

                _expandPaneOption = value;
            }
        }

        private void ModuleSelectionChanged(object parameter)
        {
            if (ModelInfoSelected != null)
            {
                ModelInfo modelInfo = _modelInfos.FirstOrDefault(x => x.ModelName == ModelInfoSelected.ModelName);

                if(modelInfo != null)
                {
                    List<ModelProperty> properties = modelInfo.ModelProperties;

                    if(properties != null)
                    {
                        FieldItems = new ObservableCollection<ModelProperty>(properties);
                    }
                }
            }
        }

        private void AddFieldItem(object parameter)
        {
            if (SelectedFieldItem != null)
            {
                var itemAlreadyAdded = SelectedFieldsItems.FirstOrDefault(item => item.Text == SelectedFieldItem.Name);
                if (itemAlreadyAdded != null)
                {
                    SelectedFieldsItems.Remove(itemAlreadyAdded);
                }

                ILinkNavigator selectFieldlinkNavigator = new DefaultLinkNavigator();
                selectFieldlinkNavigator.Commands.Add(new Uri("cmd://DeleteFieldItemCommand", UriKind.Absolute), DeleteFieldItemCommand);
                string value = SelectedFieldItem.Name;
                string bbCode = string.Format("{0}  [url=cmd://DeleteFieldItemCommand|{0}]del[/url]", value);

                SelectedFieldsItems.Add(new ListBoxItem()
                {
                    Text = value,
                    BbCode = bbCode,
                    Property = SelectedFieldItem,
                    LinkNavigator = selectFieldlinkNavigator
                });

            }
        }


        private void AddFieldItem2(object parameter)
        {
            if (SelectedFieldItem != null)
            {
                var itemToAddd = _moduleDataItems.FirstOrDefault(item => string.Equals(item.FieldName, SelectedFieldItem.Name, StringComparison.OrdinalIgnoreCase));
                if (itemToAddd == null)
                {
                    var moduleData = new ModuleData();
                    moduleData.FieldName = SelectedFieldItem.Name;
                    moduleData.Type =(Nullable.GetUnderlyingType(SelectedFieldItem.Type) ?? SelectedFieldItem.Type).Name;

                    _moduleDataItems.Add(moduleData);
                    ModuleDataItems = new ObservableCollection<ModuleData>(_moduleDataItems);
                    ShowSelectFieldDataGrid = true;
                }
            }
        }

        private void DeleteFieldItem(object parameter)
        {
            if (parameter != null)
            {
                var itemToRemove = SelectedFieldsItems.FirstOrDefault(item => item.Text == parameter.ToString());
                if (itemToRemove != null)
                {
                    SelectedFieldsItems.Remove(itemToRemove);
                }
            }
        }

        private void DeleteFieldItem2(object parameter)
        {
            if ((SelectedFieldItem) != null && (SelectedModuleDataItem != null))
            {
                ModuleData moduleData = SelectedModuleDataItem;
                _moduleDataItems.Remove(moduleData);
                SelectedModuleDataItem = null;
                ModuleDataItems = new ObservableCollection<ModuleData>(_moduleDataItems);
                ShowSelectFieldDataGrid = (_moduleDataItems.Count > 0);
                
            }
        }

        protected bool CanSend()
        {
            EnableSingleDataEntryControls = (ModelInfoSelected != null);

            if ((CurrentSugarCrmAccount == null) || !CurrentSugarCrmAccount.IsValid)
            {
                return false;
            }

            if (ModelInfoSelected == null)
            {
                return false;
            }

            if (IsSelectFieldChecked)
            {
                if ((SelectedFieldsItems == null) || (SelectedFieldsItems.Count <= 0))
                {
                    return false;
                }
            }

            return true;
        }

        private void UpdateCurrentAccount(SugarCrmAccount sugarCrmAccount)
        {
            CurrentSugarCrmAccount = sugarCrmAccount;
            Application.Current.Properties["SugarCrmAccount"] = CurrentSugarCrmAccount;
        }

        private void InitControls()
        {
            if (_container.IsRegistered<List<ModelInfo>>())
            {
                _modelInfos = _container.Resolve<List<ModelInfo>>();
            }

            if (_modelInfos != null)
            {
                ModelInfoItems = new ObservableCollection<ModelInfo>(_modelInfos);
            }

            var maxResults = new List<int>();
            maxResults.Add(10);
            maxResults.Add(25);
            maxResults.Add(100);
            maxResults.Add(250);
            maxResults.Add(500);

            PageNumber = 1;
            SelectedIndeMaxResult = 1;
            MaxResultSelected = 25;
            MaxResultItems = new ObservableCollection<int>(maxResults);

            _moduleDataItems = new List<ModuleData>();
            ModuleDataItems = new ObservableCollection<ModuleData>(_moduleDataItems);
        }
    }
}