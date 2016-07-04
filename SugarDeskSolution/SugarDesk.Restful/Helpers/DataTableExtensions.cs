
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Dynamic;
using System.Windows.Documents;
using Newtonsoft.Json.Linq;
using SugarDesk.Restful.Models;

namespace SugarDesk.Restful.Helpers
{
    internal static class DataTableExtensions 
    {
        /// <summary>
        /// Convert csv string to DataTable object
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="properties"></param>
        /// <param name="csvFile"></param>
        /// <returns>DataTable object</returns>
        public static DataTable FromCsVFile(this DataTable dataTable, List<ModelProperty> properties, string csvFile)
        {
            string[] strLines = File.ReadAllLines(csvFile);

            // get the column headers from first row
            string[] headers = strLines[0].Split(',');

            foreach (string header in headers)
            {
                ModelProperty modelProperty = properties.FirstOrDefault(x => (string.Compare(x.Name, header, StringComparison.CurrentCultureIgnoreCase) == 0));
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

    }
}

