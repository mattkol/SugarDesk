// -----------------------------------------------------------------------
// <copyright file="SugarCrmAccountService.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Biggy.Core;
    using Biggy.Data.Json;
    using Models;

    /// <summary>
    /// This class represents SugarCrmAccountService class.
    /// </summary>
    public sealed class SugarCrmAccountService
    {
        /// <summary>
        /// The csv file name containing SugarCRM urls data.
        /// </summary>
        private const string SugarCrmUrlsFile = "sugarcrm_url_list.txt";

        /// <summary>
        /// The csv file name containing SugarCRM credentials data.
        /// </summary>
        private const string SugarCrmCredentialsFile = "sugarcrm_credential_list.txt";

        /// <summary>
        /// The singleton private instance of SugarCrmAccountService.
        /// </summary>
        private static readonly SugarCrmAccountService AccountInstance = new SugarCrmAccountService();

        /// <summary>
        /// The list that holds SugarCrmUrl json objects.
        /// </summary>
        private readonly BiggyList<SugarCrmUrl> _sugarCrmUrlsList;

        /// <summary>
        /// The list that holds SugarCrmCredential json objects.
        /// </summary>
        private readonly BiggyList<SugarCrmCredential> _sugarCrmCredentialList;
        
        /// <summary>
        /// Prevents a default instance of the <see cref="SugarCrmAccountService" /> class from being created.
        /// </summary>
        private SugarCrmAccountService()
        {
            var urlStore = new JsonStore<SugarCrmUrl>();
            var credentialStore = new JsonStore<SugarCrmCredential>();
            _sugarCrmUrlsList = new BiggyList<SugarCrmUrl>(urlStore);
            _sugarCrmCredentialList = new BiggyList<SugarCrmCredential>(credentialStore);

            if (_sugarCrmUrlsList.Count <= 0)
            {
                var urlList = File.ReadAllLines(SugarCrmUrlsFile)
                                  .Skip(1)
                                  .Select(x => x.Split(','))
                                  .Select(x => new
                                  {
                                      Name = x[0].Trim().TrimStart('\"').TrimEnd('\"'),
                                      Url = x[1].Trim().TrimStart('\"').TrimEnd('\"')
                                  });

                foreach (var url in urlList)
                {
                    _sugarCrmUrlsList.Add(new SugarCrmUrl { Name = url.Name, Url = url.Url }); 
                }
            }

            if (_sugarCrmCredentialList.Count <= 0)
            {
                var credentialList = File.ReadAllLines(SugarCrmCredentialsFile)
                                  .Skip(1)
                                  .Select(x => x.Split(','))
                                  .Select(x => new
                                  {
                                      Name = x[0].Trim().TrimStart('\"').TrimEnd('\"'),
                                      Username = x[1].Trim().TrimStart('\"').TrimEnd('\"'),
                                      Password = x[2].Trim().TrimStart('\"').TrimEnd('\"'),
                                      UrlName = x[3].Trim().TrimStart('\"').TrimEnd('\"')
                                  });

                foreach (var cred in credentialList)
                {
                    _sugarCrmCredentialList.Add(new SugarCrmCredential
                    {
                        Name = cred.Name, Username = cred.Username, Password = cred.Password, UrlName = cred.UrlName
                    });
                }
            }
        }

        /// <summary>
        /// Gets the static instance of SugarCrmAccountService.
        /// </summary>
        public static SugarCrmAccountService Instance
        {
            get
            {
                return AccountInstance;
            }
        }

        /// <summary>
        /// Gets all url's available.
        /// </summary>
        public List<SugarCrmUrl> UrlList
        {
            get
            {
                return _sugarCrmUrlsList.ToList();
            }
        }

        /// <summary>
        /// Gets all credential associated with a specific url name.
        /// </summary>
        /// <param name="urlName">The group credentials url name.</param>
        /// <returns>The SugarCrmCredential collection objects.</returns>
        public List<SugarCrmCredential> GetCredentialList(string urlName)
        {
            if (string.IsNullOrEmpty(urlName))
            {
                return new List<SugarCrmCredential>();
            }

            return _sugarCrmCredentialList.Where(x => string.Equals(x.UrlName, urlName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <summary>
        /// Adds SugarCrmUrl object to the url list.
        /// </summary>
        /// <param name="url">The SugarCrmUrl object to add.</param>
        public void AddUrl(SugarCrmUrl url)
        {
            if (url == null || !url.IsValid)
            {
                return;
            }

            var urlAlreadyAdded = _sugarCrmUrlsList.FirstOrDefault(x => string.Equals(x.Name, url.Name, StringComparison.OrdinalIgnoreCase));

            if (urlAlreadyAdded == null)
            {
                _sugarCrmUrlsList.Add(url); 
            }
        }

        /// <summary>
        /// Adds SugarCrmCredential object to the credential list.
        /// </summary>
        /// <param name="credential">The SugarCrmCredential object to add.</param>
        public void AddCredntial(SugarCrmCredential credential)
        {
            if (credential == null || !credential.IsValid)
            {
                return;
            }

            var credentialAlreadyAdded = _sugarCrmCredentialList.FirstOrDefault(
                x => string.Equals(x.Name, credential.Name, StringComparison.OrdinalIgnoreCase));

            if (credentialAlreadyAdded == null)
            {
                _sugarCrmCredentialList.Add(credential);
            }
        }

        /// <summary>
        /// Removes specified SugarCrmUrl object identified by url name.
        /// </summary>
        /// <param name="name">The url name.</param>
        /// <returns>True or false (true - success, false - failure)</returns>
        public bool RemoveUrl(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            var url = _sugarCrmUrlsList.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
            if (url != null)
            {
                return _sugarCrmUrlsList.Remove(url);
            }

            return false;
        }

        /// <summary>
        /// Removes specified SugarCrmCredential object identified by url name.
        /// </summary>
        /// <param name="credential">The SugarCrmCredential object to remove.</param>
        /// <returns>True or false (true - success, false - failure)</returns>
        public bool RemoveCredntial(SugarCrmCredential credential)
        {
            if (credential == null || !credential.IsValid)
            {
                return false;
            }

            credential = _sugarCrmCredentialList.FirstOrDefault(x => x.Equals(credential));
            if (credential != null)
            {
                return _sugarCrmCredentialList.Remove(credential);
            }

            return false;
        }
    }
}
