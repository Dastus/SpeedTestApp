using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Xml.Linq;
using System.Diagnostics;
using SpeedTestApp.Models;

namespace SpeedTestApp.BL.Measurers
{
    public class Measurer : IMeasurer
    {
        public Site MeasureResponse(string url)
        {
            if (url == null)
                return null;

            string siteContent;
            XDocument xmlContent;            

            using (var client = new WebClient())
            {
                siteContent = client.DownloadString($@"http://{url}/sitemap.xml");
            }

            xmlContent = XDocument.Parse(siteContent);
            XNamespace ns = xmlContent.Root.Name.Namespace;
            IEnumerable<string> stringResult = xmlContent.Descendants(ns + "loc").
                Select(e => e.Value);
           
            return  PerformMeasures(url, stringResult);            
        }

        public static Site PerformMeasures(string url, IEnumerable<string> pagesList)
        {
            Site site = new Site();
            site.Url = url;

            Parallel.ForEach(pagesList, (page) =>
            {
                using (var client = new WebClient())
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    String result = client.DownloadString($@"http://{url}/sitemap.xml");
                    sw.Stop();

                    Measure m = new Measure
                    {
                        MeasureTime = DateTime.Now,
                        Result = (int)sw.ElapsedMilliseconds
                    };

                    Page p = new Page {PageURL = page};
                    p.Measures.Value.Add(m);
                    site.Pages.Value.Add(p);  
                }
            });

            return site;
        }
    }
}