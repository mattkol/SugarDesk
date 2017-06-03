// -----------------------------------------------------------------------
// <copyright file="RestRequest.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Models
{
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// This class represents RestRequest class.
    /// </summary>
    public class RestRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestRequest"/> class.
        /// </summary>
        public RestRequest()
        {
            Account = new SugarCrmAccount();
            Data = null;
        }

        /// <summary>
        /// Gets or sets SugarCRM account - url, username, password.
        /// </summary>
        public SugarCrmAccount Account { get; set; }

        /// <summary>
        /// Gets or sets property identifier of the model if required.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the selected field objects.
        /// </summary>
        public List<ListBoxItem> SelectedFields { get; set; }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the  maximum number of rows to return.
        /// </summary>
        public int MaxResult { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether only select fields are required.
        /// </summary>
        public bool SelectFields { get; set; }

        /// <summary>
        /// Gets or sets model metadata.
        /// </summary>
        public ModelInfo ModelInfo { get; set; }

        /// <summary>
        /// Gets or sets the datatable for create or update.
        /// </summary>
        public DataTable Data { get; set; }
    }
}
