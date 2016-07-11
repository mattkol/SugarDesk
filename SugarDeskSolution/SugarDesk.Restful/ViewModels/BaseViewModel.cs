// -----------------------------------------------------------------------
// <copyright file="BaseViewModel.cs" company="SugarDesk WPF MVVM Studio">
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
    using Core.Infrastructure.Converters;
    using FirstFloor.ModernUI.Presentation;
    using FirstFloor.ModernUI.Windows.Navigation;
    using Messages;
    using Microsoft.Practices.Unity;
    using Models;
    using Prism.Events;
    using Prism.Mvvm;
    
    /// <summary>
    /// This class represents RestViewModel class.
    /// </summary>
    public abstract class BaseViewModel : BindableBase
    {
        /// <summary>
        /// The event aggregator
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// The injected IOC container.
        /// </summary>
        private readonly IUnityContainer _container;

        /// <summary>
        /// Enum expand panel option..
        /// </summary>
        private EnumOptionType _expandPaneOption;

        /// <summary>
        /// Model info list.
        /// </summary>
        private List<ModelInfo> _modelInfos;

        /// <summary>
        /// Module date list.
        /// </summary>
        private List<ModuleData> _moduleDataItems;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="container">The Prism Unity container.</param>
        protected BaseViewModel(IEventAggregator eventAggregator, IUnityContainer container)
        {
            _eventAggregator = eventAggregator;
            _container = container;

            _eventAggregator.GetEvent<AccountMessage>().Subscribe(UpdateCurrentAccount);

            object sugarCrmAccountObj = Application.Current.Properties["SugarCrmAccount"];
            if (sugarCrmAccountObj != null)
            {
                CurrentSugarCrmAccount = (SugarCrmAccount)sugarCrmAccountObj;
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

        /// <summary>
        /// Gets the add field item command.
        /// This gets or sets the command in the ListBox using BBCode.
        /// This is needed for reads operations.
        /// </summary>
        public RelayCommand AddFieldItemCommand { get; private set; }

        /// <summary>
        /// Gets the add field item command.
        /// This gets or sets the command in the DataGrid template button using BBCode.
        /// This is needed for create, update and delete operations.
        /// </summary>
        public RelayCommand AddFieldItem2Command { get; private set; }

        /// <summary>
        /// Gets the delete field item command.
        /// This gets or sets the command in the ListBox using BBCode.
        /// This is needed for reads operations.
        /// </summary>
        public RelayCommand DeleteFieldItemCommand { get; private set; }

        /// <summary>
        /// Gets the delete field item command.
        /// This gets or sets the command in the DataGrid template button using BBCode.
        /// This is needed for create, update and delete operations.
        /// </summary>
        public RelayCommand DeleteFieldItem2Command { get; private set; }

        /// <summary>
        /// Gets the event module selection changed command.
        /// </summary>
        public RelayCommand ModuleSelectionChangedCommand { get; private set; }

        /// <summary>
        /// Gets or sets the current SugarCRM account.
        /// </summary>
        public SugarCrmAccount CurrentSugarCrmAccount { get; set; }

        /// <summary>
        /// Gets or sets the SugarCRM modules info collection.
        /// </summary>
        public ObservableCollection<ModelInfo> ModelInfoItems { get; set; }

        /// <summary>
        /// Gets or sets the SugarCRM module data item collection.
        /// </summary>
        public ObservableCollection<ModuleData> ModuleDataItems { get; set; }

        /// <summary>
        /// Gets or sets the DataGrid DataTable source created from csv.
        /// </summary>
        public DataTable ModuleFromCsvItems { get; set; }

        /// <summary>
        /// Gets or sets the DataGrid DataTable source created from csv.
        /// </summary>
        public ObservableCollection<ModelProperty> FieldItems { get; set; }

        /// <summary>
        /// Gets or sets the preset max result collection.
        /// </summary>
        public ObservableCollection<int> MaxResultItems { get; set; }

        /// <summary>
        /// Gets or sets the selected field object collection.
        /// </summary>
        public ObservableCollection<ListBoxItem> SelectedFieldsItems { get; set; }

        /// <summary>
        /// Gets or sets the DataGrid DataTable source.
        /// This is needed to dynamically create different modules in a DataGrid.
        /// </summary>
        public DataTable ModuleItems { get; set; }

        /// <summary>
        /// Gets or sets the currently selected module info object.
        /// </summary>
        public ModelInfo ModelInfoSelected { get; set; }

        /// <summary>
        /// Gets or sets the currently selected field item object.
        /// </summary>
        public ModelProperty SelectedFieldItem { get; set; }

        /// <summary>
        /// Gets or sets the currently selected module data item object.
        /// </summary>
        public ModuleData SelectedModuleDataItem { get; set; }

        /// <summary>
        /// Gets or sets the currently selected field item object.
        /// </summary>
        public ListBoxItem FieldItemSelected { get; set; }

        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets current page number to retrieved if request is per page.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets max result selected.
        /// This is the max result of data rows to return from SugarCRM Rest call.
        /// </summary>
        public int MaxResultSelected { get; set; }

        /// <summary>
        /// Gets or sets max result selected index.
        /// </summary>
        public int SelectedIndeMaxResult { get; set; }

        /// <summary>
        /// Gets or sets enum request view option.
        /// </summary>
        public EnumOptionType RequestViewOption { get; set; }

        /// <summary>
        /// Gets or sets enum response view option.
        /// </summary>
        public EnumOptionType ResponseViewOption { get; set; }

        /// <summary>
        /// Gets or sets enum create or update entries option.
        /// </summary>
        public EnumOptionType CreateOrUpdateEntriesOption { get; set; }

        /// <summary>
        /// Gets or sets enum request row height percent option.
        /// </summary>
        public GridPercentType RequestRowGridPercent { get; set; }

        /// <summary>
        /// Gets or sets enum response row height percent option.
        /// </summary>
        public GridPercentType ResponseRowGridPercent { get; set; }

        /// <summary>
        /// Gets or sets request json.
        /// </summary>
        public string RequestJson { get; set; }

        /// <summary>
        /// Gets or sets response json.
        /// </summary>
        public string ResponseJson { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether selected field is checked.
        /// </summary>
        public bool IsSelectFieldChecked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether response controls are enabled.
        /// </summary>
        public bool EnableResponseControls { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether single data entry controls are enabled.
        /// </summary>
        public bool EnableSingleDataEntryControls { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show selected fields in DataGrid.
        /// </summary>
        public bool ShowSelectFieldDataGrid { get; set; }

        /// <summary>
        /// Gets or sets the expand panel options
        /// </summary>
        public EnumOptionType ExpandPaneOption
        {
            get
            {
                return _expandPaneOption;
            }

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

        /// <summary>
        /// The can send checks.
        /// </summary>
        /// <returns>True or false.</returns>
        protected bool CanSend()
        {
            EnableSingleDataEntryControls = ModelInfoSelected != null;

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

        /// <summary>
        /// The module selection changed.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void ModuleSelectionChanged(object parameter)
        {
            if (ModelInfoSelected != null)
            {
                ModelInfo modelInfo = _modelInfos.FirstOrDefault(x => x.ModelName == ModelInfoSelected.ModelName);

                if (modelInfo != null)
                {
                    List<ModelProperty> properties = modelInfo.ModelProperties;

                    if (properties != null)
                    {
                        FieldItems = new ObservableCollection<ModelProperty>(properties);
                    }
                }
            }
        }

        /// <summary>
        /// Add field items.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
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
                string bbcode = string.Format("{0}  [url=cmd://DeleteFieldItemCommand|{0}]del[/url]", value);

                SelectedFieldsItems.Add(new ListBoxItem()
                {
                    Text = value,
                    BbCode = bbcode,
                    Property = SelectedFieldItem,
                    LinkNavigator = selectFieldlinkNavigator
                });
            }
        }

        /// <summary>
        /// Add field items.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void AddFieldItem2(object parameter)
        {
            if (SelectedFieldItem != null)
            {
                var itemToAddd = _moduleDataItems.FirstOrDefault(
                    item => string.Equals(item.FieldName, SelectedFieldItem.Name, StringComparison.OrdinalIgnoreCase));

                if (itemToAddd == null)
                {
                    var moduleData = new ModuleData();
                    moduleData.FieldName = SelectedFieldItem.Name;
                    moduleData.Type = (Nullable.GetUnderlyingType(SelectedFieldItem.Type) ?? SelectedFieldItem.Type).Name;

                    _moduleDataItems.Add(moduleData);
                    ModuleDataItems = new ObservableCollection<ModuleData>(_moduleDataItems);
                    ShowSelectFieldDataGrid = true;
                }
            }
        }

        /// <summary>
        /// Delete field items.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
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

        /// <summary>
        /// Delete field items.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void DeleteFieldItem2(object parameter)
        {
            if (SelectedFieldItem != null && (SelectedModuleDataItem != null))
            {
                ModuleData moduleData = SelectedModuleDataItem;
                _moduleDataItems.Remove(moduleData);
                SelectedModuleDataItem = null;
                ModuleDataItems = new ObservableCollection<ModuleData>(_moduleDataItems);
                ShowSelectFieldDataGrid = _moduleDataItems.Count > 0;
            }
        }

        /// <summary>
        /// Updates current account.
        /// </summary>
        /// <param name="sugarCrmAccount">The account opject to update.</param>
        private void UpdateCurrentAccount(SugarCrmAccount sugarCrmAccount)
        {
            CurrentSugarCrmAccount = sugarCrmAccount;
            Application.Current.Properties["SugarCrmAccount"] = CurrentSugarCrmAccount;
        }

        /// <summary>
        /// Initialize controls.
        /// </summary>
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
