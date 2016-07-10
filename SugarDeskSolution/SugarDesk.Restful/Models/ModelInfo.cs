// -----------------------------------------------------------------------
// <copyright file="ModelInfo.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This class represents ModelInfo class.
    /// </summary>
    public class ModelInfo
    {
        /// <summary>
        /// Gets or sets the SugarCRM model name.
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// Gets or sets model C# object type.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets model properties.
        /// </summary>
        public List<ModelProperty> ModelProperties { get; set; }
    }
}
