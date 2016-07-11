// -----------------------------------------------------------------------
// <copyright file="JsonStringExtensions.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// This class represents JsonStringExtensions class.
    /// </summary>
    public static class JsonStringExtensions 
    {
        /// <summary>
        /// Converts json string to dynamic object collections datatable.
        /// </summary>
        /// <param name="json">Json string to extend.</param>
        /// <param name="type">The type.</param>
        /// <param name="selectedFields">Selected fields.</param>
        /// <param name="selectFieldsOnly">Whether to use select fields only.</param>
        /// <returns>DataTable object</returns>
        public static DataTable ToDynamicObjects(this string json, Type type, List<string> selectedFields, bool selectFieldsOnly)
        {
            var data = new DataTable();
            JArray jarr = JArray.Parse(json);
            bool columnsAdded = false;
            foreach (JObject jobject in jarr.Children<JObject>())
            {
                object tempObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jobject.ToString(), type);

                var propertyDescriptors = new List<PropertyDescriptor>();
                if (selectFieldsOnly)
                {
                    if (selectedFields != null && selectedFields.Count > 0)
                    {
                        propertyDescriptors.AddRange(
                            TypeDescriptor.GetProperties(
                            tempObject.GetType()).Cast<PropertyDescriptor>().Where(property => selectedFields.Contains(property.Name)));
                    }
                }
                else
                {
                    propertyDescriptors.AddRange(TypeDescriptor.GetProperties(tempObject.GetType()).Cast<PropertyDescriptor>());
                }

                if (!columnsAdded)
                {
                    for (int i = 0; i < propertyDescriptors.Count; i++)
                    {
                        PropertyDescriptor prop = propertyDescriptors[i];
                        data.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    }

                    columnsAdded = true;
                }

                var values = new object[propertyDescriptors.Count];
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptors[i].GetValue(tempObject);
                }

                data.Rows.Add(values);
            }

            return data;
        }

        /// <summary>
        /// Converts json string to dynamic object datatable.
        /// </summary>
        /// <param name="json">Json string to extend.</param>
        /// <param name="type">The type.</param>
        /// <param name="selectedFields">Selected fields.</param>
        /// <param name="selectFieldsOnly">Whether to use select fields only.</param>
        /// <returns>DataTable object</returns>
        public static DataTable ToDynamicObject(this string json, Type type, List<string> selectedFields, bool selectFieldsOnly)
        {
            var data = new DataTable();
            JObject jobject = JObject.Parse(json);

            object tempObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jobject.ToString(), type);

            var propertyDescriptors = new List<PropertyDescriptor>();
            if (selectFieldsOnly)
            {
                if (selectedFields != null && selectedFields.Count > 0)
                {
                    propertyDescriptors.AddRange(
                        TypeDescriptor.GetProperties(
                        tempObject.GetType()).Cast<PropertyDescriptor>().Where(property => selectedFields.Contains(property.Name)));
                }
            }
            else
            {
                propertyDescriptors.AddRange(TypeDescriptor.GetProperties(tempObject.GetType()).Cast<PropertyDescriptor>());
            }

            for (int i = 0; i < propertyDescriptors.Count; i++)
            {
                PropertyDescriptor prop = propertyDescriptors[i];
                data.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            var values = new object[propertyDescriptors.Count];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = propertyDescriptors[i].GetValue(tempObject);
            }

            data.Rows.Add(values);

            return data;
        }
    }
}
