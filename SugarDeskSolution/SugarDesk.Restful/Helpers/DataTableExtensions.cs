// -----------------------------------------------------------------------
// <copyright file="DataTableExtensions.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using Models;
    using System.Text;

    /// <summary>
    /// This class represents DataTableExtensions class.
    /// </summary>
    public static class DataTableExtensions 
    {
        /// <summary>
        /// Convert csv string to DataTable object.
        /// </summary>
        /// <param name="dataTable">Datatabe object to be extended.</param>
        /// <param name="properties">Model properties.</param>
        /// <param name="csvFile">The csv input.</param>
        /// <returns>DataTable object</returns>
        public static DataTable FromCsVFile(this DataTable dataTable, List<ModelProperty> properties, string csvFile)
        {
            string[] strLines = File.ReadAllLines(csvFile);

            // get the column headers from first row
            string[] headers = strLines[0].Split(',');

            foreach (string header in headers)
            {
                ModelProperty modelProperty = properties.FirstOrDefault(
                    x => (string.Compare(x.Name, header, StringComparison.CurrentCultureIgnoreCase) == 0));

                if (modelProperty != null)
                {
                    dataTable.Columns.Add(header, Nullable.GetUnderlyingType(modelProperty.Type) ?? modelProperty.Type);
                }
            }

            // get records from second row
            for (int i = 1; i < strLines.Length; i++)
            {
                object[] values = strLines[i].Split(',');
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        /// <summary>
        /// Convert model headers to DataTable object.
        /// </summary>
        /// <param name="dataTable">Datatabe object to be extended.</param>
        /// <param name="properties">Model properties.</param>
        /// <returns>DataTable object</returns>
        public static DataTable FromHeaders(this DataTable dataTable, List<ModelProperty> properties)
        {
            StringBuilder builder = new StringBuilder(); 

            foreach (var property in properties)
            {
                if (property != null)
                {
                    dataTable.Columns.Add(property.Name, typeof(string));
                    Type type = Nullable.GetUnderlyingType(property.Type) ?? property.Type;
                    builder.Append(type.Name + ",");
                }
            }

            string types = builder.ToString().TrimEnd(',');
            string[] values = types.Split(',');
            dataTable.Rows.Add(values);

            return dataTable;
        }
    }
}
