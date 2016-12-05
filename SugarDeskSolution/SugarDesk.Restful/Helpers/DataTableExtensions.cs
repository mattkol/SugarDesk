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
    using Newtonsoft.Json.Linq;
    using System.Reflection;
    using SugarCrm.RestApiCalls;
    using Newtonsoft.Json;

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
        /// Convert csv string to DataTable object.
        /// </summary>
        /// <param name="dataTable">Datatabe object to be extended.</param>
        /// <param name="formData">Form data.</param>
        /// <returns>DataTable object</returns>
        public static DataTable FromFormData(this DataTable dataTable, List<FormModuleData> formData)
        {
            if ((formData == null) || (formData.Count == 0))
            {
                return dataTable;
            }

            foreach (FormModuleData item in formData)
            {
                dataTable.Columns.Add(item.FieldName, Nullable.GetUnderlyingType(item.Type) ?? item.Type);
            }

            // get  data
            object[] values = formData.Select(x => x.Value).ToArray();
            dataTable.Rows.Add(values);

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

        /// <summary>
        /// Converts a DataTable to a list with generic objects.
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="dataTable">DataTable</param>
        /// <param name="modelInfo">Model properties.</param>
        /// <returns>List with generic objects</returns>
        public static List<object> ToObjects(this DataTable dataTable, ModelInfo modelInfo, out List<string>selectedFields) 
        {
            List<object> entityList = new List<object>();
            List<string> propertyNames = modelInfo.ModelProperties.Select(x => x.Name).ToList();
            List<ModelProperty> properties = modelInfo.ModelProperties;

            selectedFields = new List<string>();

            try
            {
                foreach (DataRow dataRow in dataTable.AsEnumerable())
                {
                    JObject jobject = new JObject();
                    try
                    {
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            string columnName = column.ColumnName.Trim();
                            if (propertyNames.Contains(columnName))
                            {
                                ModelProperty property = modelInfo.ModelProperties.FirstOrDefault(x => x.Name.ToLower() == columnName.ToLower());
                                if (property != null)
                                {
                                    jobject.Add(property.JsonName, JToken.FromObject(dataRow[column]));
                                    selectedFields.Add(property.JsonName);
                                }
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }

                    object entity = JsonConvert.DeserializeObject(jobject.ToString(), modelInfo.Type);
                    entityList.Add(entity);
                }

                return entityList;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Convert datatable to Json Array object.
        /// </summary>
        /// <param name="properties">Model properties.</param>
        /// <returns>DataTable object</returns>
        public static JArray ToJson(this DataTable dataTable)
        {
            JArray jarrayData = new JArray();

            if (dataTable == null)
            {
                return jarrayData;
            }

            foreach (DataRow dataRow in dataTable.Rows)
            {
                JObject row = new JObject();
                foreach (DataColumn column in dataTable.Columns)
                {
                    row.Add(column.ColumnName.Trim(), JToken.FromObject(dataRow[column]));
                }

                jarrayData.Add(row);
            }

            return jarrayData;
        }
    }
}
