// -----------------------------------------------------------------------
// <copyright file="FormModuleData.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Models
{
    using Core.Infrastructure.Base;
    using Messages;
    using Prism.Events;
    using Prism.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class FormModuleData : ModelBase
    {
        /// <summary>
        /// The event aggregator
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormModuleData"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public FormModuleData(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
            
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets the C# property name.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the C# property name.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets a value indicating whether property is nullable.
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// Gets or sets the C# property name.
        /// </summary>
        public string TypeName { get; set; }

        public Type Type { get; set; }

        public bool IsValid
        {
            get
            {
                if (EmptyValue)
                {
                    return IsNullable;
                }

                switch(TypeName.ToLower())
                {
                    case "string":
                        return true;
                    case "int32":
                        int number;
                        return int.TryParse(Value.ToString(), out number);
                    case "sbyte":
                        sbyte sbyteValue;
                        return sbyte.TryParse(Value.ToString(), out sbyteValue);
                    case "datetime":
                        DateTime dateValue;
                        return DateTime.TryParse(Value.ToString(), out dateValue);
                }

                return false;
            }
        } 

        public string PropertyFormat
        {
            get
            {
                return string.Format("[color=gray]Property Name:{0}[/color]", FieldName);
            }
        }

        public string TypeNameFormat
        {
            get
            {
                return string.Format("[color=gray]Type:{0}[/color]", TypeName);
            }
        }

        private bool EmptyValue
        {
            get
            {
                if (Value == null)
                {
                    return true;
                }

                if(string.IsNullOrEmpty(Value.ToString()))
                {
                    return true;
                }

                return false;
            }
        }

        protected override void Validate()
        {
            List<string> errors = new List<string>();
            if (IsSelected && !IsValid)
            {
                switch (TypeName.ToLower())
                {
                    case "string":
                        errors.Add(string.Format("{0}\ncannot\nbe empty!", FieldName));
                        break;
                    case "int32":
                        errors.Add(string.Format("\"{0}\"\nis not\nvalid!", Value));
                        break;
                    case "sbyte":
                        errors.Add(string.Format("\"{0}\"\nis not\nvalid!", Value));
                        break;
                    case "datetime":
                        errors.Add(string.Format("\"{0}\"\nis not\na valid date!", Value));
                        break;
                    default:
                        errors.Add(string.Format("\"{0}\"\nis not\na valid!", Value));
                        break;
                }
            }

            ErrorsContainer.SetErrors(nameof(Value), errors);
            OnErrorsChanged(nameof(Value));

            _eventAggregator.GetEvent<UpdateMessage>().Publish(true);
        }
    }
}
