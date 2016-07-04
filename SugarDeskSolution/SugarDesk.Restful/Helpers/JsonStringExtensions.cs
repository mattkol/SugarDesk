using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ComponentModel;
using System.Dynamic;
using System.Windows.Documents;
using Newtonsoft.Json.Linq;
using SugarDesk.Restful.Models;

namespace SugarDesk.Restful.Helpers
{
    internal static class JsonStringExtensions 
    {
        /// <summary>
        /// Gets enity by id
        /// </summary>
        /// <param name="json"></param>
        /// <param name="type"></param>
        /// <param name="selectedFields"></param>
        /// <param name="selectFieldsOnly"></param>
        /// <returns>DataTable object</returns>
        public static DataTable ToDynamicObjects(this string json, Type type, List<string> selectedFields, bool selectFieldsOnly)
        {
            var data = new DataTable();
            JArray jarr = JArray.Parse(json);
            bool columnsAdded = false;
            foreach (JObject jObject in jarr.Children<JObject>())
            {
                object tempObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jObject.ToString(), type);

                var propertyDescriptors = new List<PropertyDescriptor>();
                if (selectFieldsOnly)
                {
                    if (selectedFields != null && selectedFields.Count > 0)
                    {
                        propertyDescriptors.AddRange(TypeDescriptor.GetProperties(tempObject.GetType()).Cast<PropertyDescriptor>().Where(property => selectedFields.Contains(property.Name)));
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
        /// Gets enity by id
        /// </summary>
        /// <param name="json"></param>
        /// <param name="type"></param>
        /// <param name="selectedFields"></param>
        /// <param name="selectFieldsOnly"></param>
        /// <returns>DataTable object</returns>
        public static DataTable ToDynamicObject(this string json, Type type, List<string> selectedFields, bool selectFieldsOnly)
        {
            var data = new DataTable();
            JObject jObject = JObject.Parse(json);

            object tempObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jObject.ToString(), type);

            var propertyDescriptors = new List<PropertyDescriptor>();
            if (selectFieldsOnly)
            {
                if (selectedFields != null && selectedFields.Count > 0)
                {
                    propertyDescriptors.AddRange(TypeDescriptor.GetProperties(tempObject.GetType()).Cast<PropertyDescriptor>().Where(property => selectedFields.Contains(property.Name)));
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

