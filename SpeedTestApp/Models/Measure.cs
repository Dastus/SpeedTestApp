using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedTestApp.Models
{
    public class Measure : IApplicationModel, IComparable<Measure>
    {
        public int MeasureId { get; set; }
        public int PageID { get; set; }
        public int Result { get; set; }
        public Nullable<System.DateTime> MeasureTime { get; set; }

        public int CompareTo(Measure m)
        {
            if (this.Result > m.Result)
                return 1;
            if (this.Result < m.Result)
                return -1;
            return 0;
        }
    }
}