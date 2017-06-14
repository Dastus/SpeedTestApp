using System;
using SpeedTestApp.Models;
using SpeedTestApp.BL.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Xml.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SpeedTestApp.BL.Service
{
    public class MeasureManager : IMeasureManager
    {
        private IMeasureRepository measureRepository;

        public MeasureManager(IMeasureRepository repository)
        {
            this.measureRepository = repository;
        }

        public void AddMeasure(Measure measure, Page page)
        {
            if (measure == null || page == null)
                throw new ArgumentNullException("Measure or Page is Null.");
            else
                measureRepository.AddMeasure(measure, page);
        }

        public Site MeasureResponse(string url)
        {
            if (url == null)
                return null;
            
            string siteContent;
            XDocument xmlContent;
            try
            {
                using (var client = new WebClient())
                {
                    siteContent = client.DownloadString($@"http://{url}/sitemap.xml");
                }

                xmlContent = XDocument.Parse(siteContent);
                XNamespace ns = xmlContent.Root.Name.Namespace;
                IEnumerable<string> stringResult = xmlContent.Descendants(ns + "loc").
                    Select(e => e.Value);

                return PerformMeasures(url, stringResult);
            }
            catch (WebException)
            {
                
                HashSet<string> sitemap = new HashSet<string>();
                GetSiteMapByUrl($@"https://{url}", ref sitemap);
                Site site = PerformMeasures(url, sitemap);                

                return (site.Pages.Value.Count == 0 ? null : site);
            }            
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
                    try
                    {
                        sw.Start();
                        String result = client.DownloadString($@"http://{url}/sitemap.xml");
                        sw.Stop();
                    }
                    catch (WebException) { }

                    Measure m = new Measure
                    {
                        MeasureTime = DateTime.Now,
                        Result = (int)sw.ElapsedMilliseconds
                    };

                    Page p = new Page { PageURL = page , MinResponse = m.Result, MaxResponse = m.Result };
                    p.Measures.Value.Add(m);

                    site.Pages.Value.Add(p);
                }
            });

            return site;
        }

        private static void GetSiteMapByUrl(string url, ref HashSet<string> sitemap)
        {
            HashSet<string> result = new HashSet<string>();
            HashSet<string> localSiteMap = sitemap;
            Match m;
            string inputString;
            string pattern = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))";            

            try
            {
                using (var client = new WebClient())
                {
                    inputString = client.DownloadString(url);
                }

                m = Regex.Match(inputString, pattern,
                    RegexOptions.IgnoreCase | RegexOptions.Compiled,
                    TimeSpan.FromSeconds(5));

                while (m.Success)
                {
                    if (m.Groups[1].ToString().Contains(url))
                        result.Add(m.Groups[1].ToString());
                    m = m.NextMatch();
                }
            }
            catch (WebException) { }
            catch (ArgumentException) { }
            catch (RegexMatchTimeoutException) { }            

            if (result.Count > 0)
            {
                var newPages = result.Where(x => !localSiteMap.Contains(x));

                foreach (var p in newPages)
                {
                    sitemap.Add(p);
                    GetSiteMapByUrl(p, ref sitemap);
                }
            }
        }

    }    
}