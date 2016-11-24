// -----------------------------------------------------------------------
// <copyright file="FormModuleData.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Models
{
    using Core.Infrastructure.Base;
    using System;
    using System.Collections.Generic;

    public class FormModuleData : ModelBase
    {
        private const string NullValue = "null";

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
                bool isNullValue = Value.ToString().ToLower() == NullValue;
                if (IsNullable && (EmptyValue || isNullValue))
                {
                    return true;
                }

                if (EmptyValue)
                {
                    return false;
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

        protected override void Validate()
        {
            List<string> errors = new List<string>();
            if (IsSelected && !IsValid)
            {
                errors.Add("errors");
            }

            ErrorsContainer.SetErrors(nameof(Value), errors);
            OnErrorsChanged(nameof(Value));
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
    }
}
