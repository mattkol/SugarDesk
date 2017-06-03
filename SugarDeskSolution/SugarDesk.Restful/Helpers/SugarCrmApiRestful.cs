// -----------------------------------------------------------------------
// <copyright file="SugarCrmApiRestful.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Helpers
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json.Linq;
    using System.Net;
    using System;
    using SugarRestSharp;

    /// <summary>
    /// This class represents SugarCrmApiRestful class.
    /// </summary>
    public static class SugarCrmApiRestful
    {
        /// <summary>
        /// This GetAll request.
        /// </summary>
        /// <param name="restRequest">SugarCRM Rest request parameters.</param>
        /// <returns>The task response object.</returns>
        public static Task<RestResponse> GetAll(RestRequest restRequest)
        {
            return Task.Run(() =>
                {
                    var response = new RestResponse();

                    var request = new SugarRestRequest();
                    request.RequestType = RequestType.BulkRead;
                    request.ModuleName = restRequest.ModelInfo.ModelName;
                    request.Url = restRequest.Account.Url;
                    request.Username = restRequest.Account.Username;
                    request.Password = restRequest.Account.Password;

                    bool selectedFieldsOnly = false;
                    var properties = new List<ModelProperty>();
                    if (restRequest.SelectFields)
                    {
                        if (restRequest.SelectedFields != null && restRequest.SelectedFields.Count > 0)
                        {
                            properties = restRequest.SelectedFields.Select(x => x.Property).ToList();
                            if (properties.Count > 0)
                            {
                                request.Options.SelectFields = properties.Select(x => x.JsonName).ToList();
                                selectedFieldsOnly = true;
                            }
                        }
                    }

                    // If zero (0) default value is used
                    if (restRequest.MaxResult > 0)
                    {
                        request.Options.MaxResult = restRequest.MaxResult;
                    }

                    var client = new SugarRestClient();
                    SugarRestResponse sugarRestResponse = client.Execute(request);

                    response.Data = new DataTable();
                    if (sugarRestResponse != null)
                    {
                        var selectedProperties = new List<string>();

                        if (selectedFieldsOnly)
                        {
                            selectedProperties = properties.Select(x => x.Name).ToList();
                        }

                        if (!string.IsNullOrEmpty(sugarRestResponse.JData))
                        {
                            response.Data = sugarRestResponse.JData.ToDynamicObjects(
                                restRequest.ModelInfo.Type, selectedProperties, selectedFieldsOnly);
                        }

                        response.JsonRawRequest = JToken.Parse(
                            sugarRestResponse.JsonRawRequest).ToString(Newtonsoft.Json.Formatting.Indented);

                        response.JsonRawResponse = JToken.Parse(
                            sugarRestResponse.JsonRawResponse).ToString(Newtonsoft.Json.Formatting.Indented);
                    }

                    return response;
            });
        }

        /// <summary>
        /// This GetByPage request.
        /// </summary>
        /// <param name="restRequest">SugarCRM Rest request parameters.</param>
        /// <returns>The task response object.</returns>
        public static Task<RestResponse> GetByPage(RestRequest restRequest)
        {
            return Task.Run(() =>
            {
                var response = new RestResponse();

                var request = new SugarRestRequest();
                request.RequestType = RequestType.PagedRead;
                request.ModuleName = restRequest.ModelInfo.ModelName;
                request.Url = restRequest.Account.Url;
                request.Username = restRequest.Account.Username;
                request.Password = restRequest.Account.Password;

                bool selectedFieldsOnly = false;
                var properties = new List<ModelProperty>();
                if (restRequest.SelectFields)
                {
                    if (restRequest.SelectedFields != null && restRequest.SelectedFields.Count > 0)
                    {
                        properties = restRequest.SelectedFields.Select(x => x.Property).ToList();
                        if (properties.Count > 0)
                        {
                            request.Options.SelectFields = properties.Select(x => x.JsonName).ToList();
                            selectedFieldsOnly = true;
                        }
                    }
                }

                request.Options.CurrentPage = restRequest.CurrentPage;

                // If zero (0) default value is used
                if (restRequest.MaxResult > 0)
                {
                    request.Options.NumberPerPage = restRequest.MaxResult;
                }

                var client = new SugarRestClient();
                SugarRestResponse sugarRestResponse = client.Execute(request);

                response.Data = new DataTable();
                if (sugarRestResponse != null)
                {
                    var selectedProperties = new List<string>();

                    if (selectedFieldsOnly)
                    {
                        selectedProperties = properties.Select(x => x.Name).ToList();
                    }

                    if (!string.IsNullOrEmpty(sugarRestResponse.JData))
                    {
                        response.Data = sugarRestResponse.JData.ToDynamicObjects(restRequest.ModelInfo.Type, selectedProperties, selectedFieldsOnly);
                    }

                    response.JsonRawRequest = JToken.Parse(sugarRestResponse.JsonRawRequest).ToString(Newtonsoft.Json.Formatting.Indented);
                    response.JsonRawResponse = JToken.Parse(sugarRestResponse.JsonRawResponse).ToString(Newtonsoft.Json.Formatting.Indented);
                }

                return response;
            });
        }

        /// <summary>
        /// This GetById request.
        /// </summary>
        /// <param name="restRequest">SugarCRM Rest request parameters.</param>
        /// <returns>The task response object.</returns>
        public static Task<RestResponse> GetById(RestRequest restRequest)
        {
            return Task.Run(() =>
            {
                var response = new RestResponse();

                var request = new SugarRestRequest();
                request.RequestType = RequestType.ReadById;
                request.ModuleName = restRequest.ModelInfo.ModelName;
                request.Url = restRequest.Account.Url;
                request.Username = restRequest.Account.Username;
                request.Password = restRequest.Account.Password;

                request.Parameter = restRequest.Id;

                bool selectedFieldsOnly = false;
                var properties = new List<ModelProperty>();
                if (restRequest.SelectFields)
                {
                    if (restRequest.SelectedFields != null && restRequest.SelectedFields.Count > 0)
                    {
                        properties = restRequest.SelectedFields.Select(x => x.Property).ToList();
                        if (properties.Count > 0)
                        {
                            request.Options.SelectFields = properties.Select(x => x.JsonName).ToList();
                            selectedFieldsOnly = true;
                        }
                    }
                }

                var client = new SugarRestClient();
                SugarRestResponse sugarRestResponse = client.Execute(request);

                response.Data = new DataTable();
                if (sugarRestResponse != null)
                {
                    var selectedProperties = new List<string>();

                    if (selectedFieldsOnly)
                    {
                        selectedProperties = properties.Select(x => x.Name).ToList();
                    }

                    if (!string.IsNullOrEmpty(sugarRestResponse.JData))
                    {
                        response.Data = sugarRestResponse.JData.ToDynamicObject(restRequest.ModelInfo.Type, selectedProperties, selectedFieldsOnly);
                    }

                    response.JsonRawRequest = JToken.Parse(sugarRestResponse.JsonRawRequest).ToString(Newtonsoft.Json.Formatting.Indented);
                    response.JsonRawResponse = JToken.Parse(sugarRestResponse.JsonRawResponse).ToString(Newtonsoft.Json.Formatting.Indented);
                }

                return response;
            });
        }

        /// <summary>
        /// This Create request.
        /// </summary>
        /// <param name="restRequest">SugarCRM Rest request parameters.</param>
        /// <returns>The task response object.</returns>
        public static Task<RestResponse> Create(RestRequest restRequest)
        {
            return Task.Run(() =>
            {
                var response = new RestResponse();

                var request = new SugarRestRequest();
                request.RequestType = RequestType.Create;
                request.ModuleName = restRequest.ModelInfo.ModelName;
                request.Url = restRequest.Account.Url;
                request.Username = restRequest.Account.Username;
                request.Password = restRequest.Account.Password;

                var client = new SugarRestClient();

                List<string> selectedFields;
                List<object> dataList = restRequest.Data.ToObjects(restRequest.ModelInfo, out selectedFields);
                request.Options.SelectFields = selectedFields;

                if (dataList == null)
                {
                    return response;
                }

                SugarRestResponse sugarRestResponse = new SugarRestResponse();
                if (dataList.Count == 1)
                {
                    request.Parameter = dataList;
                    sugarRestResponse = client.Execute(request);
                    response.Id = (string)sugarRestResponse.Data;
                }
                else
                {
                    request.Parameter = dataList;
                    sugarRestResponse = client.Execute(request);
                }


                try
                {
                    response.JsonRawRequest = JToken.Parse(sugarRestResponse.JsonRawRequest).ToString(Newtonsoft.Json.Formatting.Indented);
                }
                catch (Exception exception)
                {
                    response.Failure = true;
                    response.JsonRawRequest = exception.Message;
                }

                if (sugarRestResponse.StatusCode == HttpStatusCode.OK)
                {
                    try
                    {
                        response.JsonRawResponse = JToken.Parse(sugarRestResponse.JsonRawResponse).ToString(Newtonsoft.Json.Formatting.Indented);
                    }
                    catch (Exception exception)
                    {
                        response.Failure = true;
                        response.JsonRawResponse = exception.Message;
                    }
                }
                else
                {
                    response.Failure = true;

                    if (sugarRestResponse.Error != null)
                    {
                        response.JsonRawResponse = sugarRestResponse.Error.Message;
                    }
                    else
                    {
                        response.JsonRawResponse = "An error occurs processing request!";
                    }

                }

                return response;
            });
        }

        /// <summary>
        /// This Update request.
        /// </summary>
        /// <param name="restRequest">SugarCRM Rest request parameters.</param>
        /// <returns>The task response object.</returns>
        public static Task<RestResponse> Update(RestRequest restRequest)
        {
            return Task.Run(() =>
            {
                var response = new RestResponse();

                var request = new SugarRestRequest();
                request.RequestType = RequestType.Update;
                request.ModuleName = restRequest.ModelInfo.ModelName;
                request.Url = restRequest.Account.Url;
                request.Username = restRequest.Account.Username;
                request.Password = restRequest.Account.Password;

                var client = new SugarRestClient();

                List<string> selectedFields;
                List<object> dataList = restRequest.Data.ToObjects(restRequest.ModelInfo, out selectedFields);
                request.Options.SelectFields = selectedFields;

                if (dataList == null)
                {
                    return response;
                }

                SugarRestResponse sugarRestResponse = new SugarRestResponse();
                if (dataList.Count == 1)
                {
                    request.Parameter = dataList;
                    sugarRestResponse = client.Execute(request);
                    response.Id = (string)sugarRestResponse.Data;
                }
                else
                {
                    request.Parameter = dataList;
                    sugarRestResponse = client.Execute(request);
                }

                response.JsonRawRequest = JToken.Parse(sugarRestResponse.JsonRawRequest).ToString(Newtonsoft.Json.Formatting.Indented);
                response.JsonRawResponse = JToken.Parse(sugarRestResponse.JsonRawResponse).ToString(Newtonsoft.Json.Formatting.Indented);

                return response;
            });
        }

        /// <summary>
        /// This Delete request.
        /// </summary>
        /// <param name="restRequest">SugarCRM Rest request parameters.</param>
        /// <returns>The task response object.</returns>
        public static Task<RestResponse> Delete(RestRequest restRequest)
        {
            return Task.Run(() =>
            {
                var response = new RestResponse();

                var request = new SugarRestRequest();
                request.RequestType = RequestType.Delete;
                request.ModuleName = restRequest.ModelInfo.ModelName;
                request.Url = restRequest.Account.Url;
                request.Username = restRequest.Account.Username;
                request.Password = restRequest.Account.Password;

                request.Parameter = restRequest.Id;

                var client = new SugarRestClient();
                SugarRestResponse sugarRestResponse = client.Execute(request);

                response.Id = (string)sugarRestResponse.Data;
                response.JsonRawRequest = JToken.Parse(sugarRestResponse.JsonRawRequest).ToString(Newtonsoft.Json.Formatting.Indented);
                response.JsonRawResponse = JToken.Parse(sugarRestResponse.JsonRawResponse).ToString(Newtonsoft.Json.Formatting.Indented);

                return response;
            });
        }
    }
}
