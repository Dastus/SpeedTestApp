using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedTestApp.Models
{
    public class Page : IApplicationModel, IComparable<Page>
    {
        public int PageId { get; set; }
        public int SiteID { get; set; }
        public string PageURL { get; set; }
        public Lazy<List<Measure>> Measures = new Lazy<List<Measure>>();

        public int CompareTo(Page otherPage)
        {
            int thisResult = otherPage.Measures.Value.Max().Result;
            int otherResult = this.Measures.Value.Max().Result;

            if (thisResult > otherResult)
                return 1;
            if (thisResult < otherResult)
                return -1;
            return 0;
        }
    }
}