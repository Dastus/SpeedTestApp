using System;
using System.Collections.Generic;

namespace SpeedTestApp.Models
{
    public class Page : IApplicationModel, IComparable<Page>
    {
        public int PageId { get; set; }
        public int SiteID { get; set; }
        public int MaxResponse { get; set; }
        public int MinResponse { get; set; }
        public string PageURL { get; set; }
        public Lazy<List<Measure>> Measures = new Lazy<List<Measure>>();

        public int CompareTo(Page otherPage)
        {
            if (this.MaxResponse > otherPage.MaxResponse)
                return 1;
            if (this.MaxResponse < otherPage.MaxResponse)
                return -1;
            return 0;
        }

    }
}