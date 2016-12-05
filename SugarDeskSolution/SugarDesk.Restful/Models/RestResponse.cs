// -----------------------------------------------------------------------
// <copyright file="RestResponse.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Models
{
    using System.Data;

    /// <summary>
    /// This class represents RestResponse class.
    /// </summary>
    public class RestResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestResponse"/> class.
        /// </summary>
        public RestResponse()
        {
            Data = new DataTable();
            JsonRawRequest = "No valid data returned!";
            JsonRawResponse = "No valid data returned!";
        }

        /// <summary>
        /// Gets or sets succes status.
        /// </summary>
        public bool Failure { get; set; }

        /// <summary>
        /// Gets or sets data.
        /// </summary>
        public DataTable Data { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets json raw request.
        /// </summary>
        public string JsonRawRequest { get; set; }

        /// <summary>
        /// Gets or sets json raw response.
        /// </summary>
        public string JsonRawResponse { get; set; } 
    }
}
