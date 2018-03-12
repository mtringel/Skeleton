using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TopTal.JoggingApp.Service.Models.Helpers
{
    public class ServiceResult : Model
    {
        public HttpStatusCode StatusCode { get; set; }

        public string StatusDescription { get; set; }

        public string DetailedErrorMessage { get; set; }

        public ServiceResult()
            : this (HttpStatusCode.OK, null)
        {            
        }

        public ServiceResult(HttpStatusCode statusCode, string statusDescription)
        {
            this.StatusCode = statusCode;
            this.StatusDescription = statusDescription;
        }

        public ServiceResult(HttpStatusCode statusCode)
            : this(statusCode, null)
        {
        }
    }
}
