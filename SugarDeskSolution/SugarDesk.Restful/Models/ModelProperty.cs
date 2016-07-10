// -----------------------------------------------------------------------
// <copyright file="ModelProperty.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Models
{
    using System;

    /// <summary>
    /// This class represents ModelProperty class.
    /// </summary>
    public class ModelProperty
    {
        /// <summary>
        /// Gets or sets the C# property name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the property name in json.
        /// </summary>
        public string JsonName { get; set; }

        /// <summary>
        /// Gets or sets property C# object type.
        /// </summary>
        public Type Type { get; set; }
    }
}
