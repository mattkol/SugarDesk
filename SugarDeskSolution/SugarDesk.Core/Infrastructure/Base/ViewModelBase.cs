// -----------------------------------------------------------------------
// <copyright file="ViewModelBase.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Core.Infrastructure.Base
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using FluentValidation.Results;
    using Prism.Mvvm;
    using FirstFloor.ModernUI.Presentation;
    using Validators;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// This class represents ViewModelBase class.
    /// </summary>
    public abstract class ViewModelBase : BindableBase, INotifyDataErrorInfo
    {
        object lockObj = new object();
        ValidationErrorsContainer<string> errorsContainer;

        protected new event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        protected IModelValidator Validator { get; set; }
        protected FrameworkElement ZoomableView { get; set; }

        public Transform ZoomLayoutTransform { get; set; }

        protected ValidationErrorsContainer<string> ErrorsContainer
        {
            get
            {
                if (errorsContainer == null)
                {
                    errorsContainer = new ValidationErrorsContainer<string>(pn => OnErrorsChanged(pn));
                }

                return errorsContainer;
            }
        }

        public bool HasErrors
        {
            get { return ErrorsContainer.HasErrors; }
        }


        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Validate();
        }

        public ViewModelBase()
        {
            OnLoadedCommand = new RelayCommand(OnLoaded);
            OnPreviewKeyDownCommand = new RelayCommand(OnPreviewKeyDown);
            OnPreviewKeyUpCommand = new RelayCommand(OnPreviewKeyUp);
            OnPreviewMouseWheelCommand = new RelayCommand(OnPreviewMouseWheel);
        }

        /// <summary>
        /// Gets the delete url command.
        /// </summary>
        public RelayCommand OnLoadedCommand { get; private set; }
        public RelayCommand OnPreviewKeyDownCommand { get; private set; }
        public RelayCommand OnPreviewKeyUpCommand { get; private set; }
        public RelayCommand OnPreviewMouseWheelCommand { get; private set; }

        public void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return ErrorsContainer.GetErrors(propertyName);
        }

        void Validate()
        {
            if (Validator != null)
            {
                lock (lockObj)
                {
                    ValidationResult results = Validator.Validate();
                    if (results != null)
                    {
                        // reset error container
                        ErrorsContainer.ClearAllErrors();

                        List<string> errorPropertyNames = results.Errors.Select(x => x.PropertyName).ToList();
                        if ((errorPropertyNames != null) && (errorPropertyNames.Count > 0))
                        {
                            foreach (var propertyName in errorPropertyNames)
                            {
                                List<string> errorMessages = results.Errors.Where(pn => pn.PropertyName.ToLower() == propertyName.ToLower()).Select(x => x.ErrorMessage).ToList();
                                ErrorsContainer.SetErrors(propertyName, errorMessages);
                                OnErrorsChanged(propertyName);
                            }
                        }
                    }
                }
            }
        }


        private void OnLoaded(object parameter)
        {
            if (ZoomableView == null)
            {
                if (parameter != null)
                {
                    if (parameter is FrameworkElement)
                    {
                        ZoomableView = (FrameworkElement) parameter;
                        ZoomableView.Focusable = true;
                    }
                }
            }

            Validate();
        }

        #region Zooming

        private void OnPreviewKeyDown(object parameter)
        {
        }

        private void OnPreviewKeyUp(object parameter)
        {
        }

        private void OnPreviewMouseWheel(object parameter)
        {
        }

        #endregion
    }
}
