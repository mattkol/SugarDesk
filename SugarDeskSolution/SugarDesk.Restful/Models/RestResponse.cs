using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugarDesk.Restful.Models
{
    public class RestResponse
    {
        public RestResponse()
        {
            Data = new DataTable();
            JsonRawRequest = "No valid data returned!";
            JsonRawResponse = "No valid data returned!";
        }

        public DataTable Data { get; set; }
        public string Id { get; set; }
        public string JsonRawRequest { get; set; }
        public string JsonRawResponse { get; set; } 
    }
}
