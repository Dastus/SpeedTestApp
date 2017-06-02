using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpeedTestApp.Models;

namespace SpeedTestApp.UI.ViewModels
{
    public class MeasuresViewModel
    {
        public string Site { get; set; }        
        public List<Page> HistoricalResults;
        public List<DisplayPageInfo> CurrentResultsGraph;
    }
}