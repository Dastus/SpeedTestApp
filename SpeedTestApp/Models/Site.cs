using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedTestApp.Models
{
    public class Site : IApplicationModel
    {
        public int SiteId { get; set; }
        public string Url { get; set; }
        public Lazy<List<Page>> Pages = new Lazy<List<Page>>();
    }
}