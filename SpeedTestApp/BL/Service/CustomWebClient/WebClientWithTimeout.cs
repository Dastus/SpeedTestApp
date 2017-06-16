using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace SpeedTestApp.BL.Service.CustomWebClient
{
    public class WebClientWithTimeout : WebClient
    {
        public int Timeout { get; set; }

        public WebClientWithTimeout() : this(8000) { }

        public WebClientWithTimeout(int timeout)
        {
            this.Timeout = timeout;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request != null)
            {
                request.Timeout = this.Timeout;
            }
            return request;
        }
    }
}