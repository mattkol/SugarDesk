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
    sealed class SugarCrmAccountService
    {
        static readonly SugarCrmAccountService _instance = new SugarCrmAccountService();
        private readonly BiggyList<SugarCrmUrl> _sugarCrmUrlsList = null; 
        private readonly BiggyList<SugarCrmCredential> _sugarCrmCredentialList = null;
        private const string SugarCrmUrlsFile = "sugarcrm_url_list.txt";
        private const string SugarCrmCredentialsFile = "sugarcrm_credential_list.txt";

        /// <summary>
        /// Prevents a default instance of the <see cref="SugarCrmAccountService" /> class from being created.
        /// </summary>
        SugarCrmAccountService()
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

        public static SugarCrmAccountService Instance
        {
            get
            {
                return _instance;
            }
        }

        public List<SugarCrmUrl> UrlList
        {
            get
            {
                return _sugarCrmUrlsList.ToList();
            }
        }

        public List<SugarCrmCredential> GetCredentialList(string urlName)
        {
            if (string.IsNullOrEmpty(urlName))
            {
                return new List<SugarCrmCredential>();
            }


            return _sugarCrmCredentialList.Where(x => string.Equals(x.UrlName, urlName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

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
