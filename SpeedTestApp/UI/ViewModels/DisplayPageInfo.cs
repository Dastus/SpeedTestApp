using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedTestApp.UI.ViewModels
{
    public class DisplayPageInfo
    {
        public string URL { get; set; }
        public int MeasureResult { get; set; }
        public int Percent { get; set; }
    }
}