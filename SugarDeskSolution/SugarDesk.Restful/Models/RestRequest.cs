using System;
using System.Collections.Generic;

namespace SugarDesk.Restful.Models
{
    public class RestRequest
    {
        public RestRequest()
        {
            Account = new SugarCrmAccount();
        }

        public SugarCrmAccount Account { get; set; }
        public string Id { get; set; } 
        public string ModuleName { get; set; } 
        public List<ListBoxItem> SelectedFields { get; set; }
        public int CurrentPage { get; set; }
        public int MaxResult { get; set; }
        public Type Type { get; set; }
        public bool SelectFields { get; set; } 
    }
}
