// -----------------------------------------------------------------------
// <copyright file="ModelBase.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Core.Infrastructure.Base
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using Prism.Mvvm;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// This class represents ModelBase class.
    /// </summary>
    public abstract class ModelBase : BindableBase, INotifyDataErrorInfo
    {
        ErrorsContainer<string> errorsContainer;

        protected new event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected ErrorsContainer<string> ErrorsContainer
        {
            get
            {
                if (errorsContainer == null)
                {
                    errorsContainer = new ErrorsContainer<string>(pn => OnErrorsChanged(pn));
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

        public void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return ErrorsContainer.GetErrors(propertyName);
        }

        protected virtual void Validate()
        {
        }
    }
}