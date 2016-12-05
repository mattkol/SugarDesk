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
    using SugarCrm.RestfulCRUD;
    using SugarCrm.RestApiCalls;
    using System.Net;
    using System;

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
                    SugarRestResponse sugarRestResponse = client.GetAll(request);

                    response.Data = new DataTable();
                    if (!string.IsNullOrEmpty(sugarRestResponse.Content))
                    {
                        var selectedProperties = new List<string>();

                        if (selectedFieldsOnly)
                        {
                            selectedProperties = properties.Select(x => x.Name).ToList();
                        }

                        response.Data = sugarRestResponse.Content.ToDynamicObjects(
                            restRequest.ModelInfo.Type, selectedProperties, selectedFieldsOnly);

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
                SugarRestResponse sugarRestResponse = client.GetPaged(request);

                response.Data = new DataTable();
                if (!string.IsNullOrEmpty(sugarRestResponse.Content))
                {
                    var selectedProperties = new List<string>();

                    if (selectedFieldsOnly)
                    {
                        selectedProperties = properties.Select(x => x.Name).ToList();
                    }

                    response.Data = sugarRestResponse.Content.ToDynamicObjects(restRequest.ModelInfo.Type, selectedProperties, selectedFieldsOnly);
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
                request.ModuleName = restRequest.ModelInfo.ModelName;
                request.Url = restRequest.Account.Url;
                request.Username = restRequest.Account.Username;
                request.Password = restRequest.Account.Password;

                request.Id = restRequest.Id;

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
                SugarRestResponse sugarRestResponse = client.GetById(request);

                response.Data = new DataTable();
                if (!string.IsNullOrEmpty(sugarRestResponse.Content))
                {
                    var selectedProperties = new List<string>();

                    if (selectedFieldsOnly)
                    {
                        selectedProperties = properties.Select(x => x.Name).ToList();
                    }

                    response.Data = sugarRestResponse.Content.ToDynamicObject(restRequest.ModelInfo.Type, selectedProperties, selectedFieldsOnly);
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
                    request.Data = dataList;
                    sugarRestResponse = client.Insert(request);
                    response.Id = sugarRestResponse.Id;
                }
                else
                {
                    request.Data = dataList;
                    sugarRestResponse = client.Inserts(request);
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
                    request.Data = dataList;
                    sugarRestResponse = client.Update(request);
                    response.Id = sugarRestResponse.Id;
                }
                else
                {
                    request.Data = dataList;
                    sugarRestResponse = client.Updates(request);
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
                request.ModuleName = restRequest.ModelInfo.ModelName;
                request.Url = restRequest.Account.Url;
                request.Username = restRequest.Account.Username;
                request.Password = restRequest.Account.Password;

                request.Id = restRequest.Id;

                var client = new SugarRestClient();
                SugarRestResponse sugarRestResponse = client.Delete(request);

                response.Id = sugarRestResponse.Id;
                response.JsonRawRequest = JToken.Parse(sugarRestResponse.JsonRawRequest).ToString(Newtonsoft.Json.Formatting.Indented);
                response.JsonRawResponse = JToken.Parse(sugarRestResponse.JsonRawResponse).ToString(Newtonsoft.Json.Formatting.Indented);

                return response;
            });
        }
    }
}
