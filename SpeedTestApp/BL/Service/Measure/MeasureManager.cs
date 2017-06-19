using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Xml.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using SpeedTestApp.Models;
using SpeedTestApp.BL.Repository;
using SpeedTestApp.BL.Service.CustomWebClient;
using System.Runtime.Caching;

namespace SpeedTestApp.BL.Service
{
    public class MeasureManager : IMeasureManager
    {
        private IMeasureRepository measureRepository;
        private MemoryCache memoryCache = MemoryCache.Default;

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
            if (url.StartsWith("http://"))
                url = url.Remove(0, 7);
            if (url.StartsWith("https://"))
                url = url.Remove(0, 8);    

            string siteContent;
            XDocument xmlContent;
            try
            {                
                using (var client = new WebClientWithTimeout())
                {
                    siteContent = client.DownloadString(url);
                }
                try
                {
                    xmlContent = XDocument.Parse(siteContent);
                    XNamespace ns = xmlContent.Root.Name.Namespace;
                    IEnumerable<string> stringResult = xmlContent.Descendants(ns + "loc").
                        Select(e => e.Value);

                    return PerformMeasures(url, stringResult);
                }
                catch (Exception)
                {
                    throw new WebException();
                }                
                
            }
            catch (WebException)
            {                
                HashSet<string> sitemap = new HashSet<string>();
                               

                if (memoryCache.Get($"sitemap_{url}") != null)
                    sitemap = memoryCache.Get($"sitemap_{url}") as HashSet<string>; 
                else
                {
                    GetSiteMapByUrl($@"https://{url}", ref sitemap);
                    GetSiteMapByUrl($@"http://{url}", ref sitemap);

                    memoryCache.Add($"sitemap_{url}", sitemap, DateTime.Now.AddMinutes(5));                                    
                }

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
                using (var client = new WebClientWithTimeout())
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
                using (var client = new WebClientWithTimeout(8000))
                {                    
                    inputString = client.DownloadString(url);
                }

                m = Regex.Match(inputString, pattern,
                    RegexOptions.IgnoreCase | RegexOptions.Compiled,
                    TimeSpan.FromSeconds(5));

                while (m.Success)
                {
                    string currentMatch = m.Groups[1].ToString();
                    if (currentMatch.Contains(url))
                        result.Add(currentMatch);
                    else if (currentMatch.Length > 1
                        && currentMatch.StartsWith("/")
                        && currentMatch.EndsWith("/"))
                        result.Add(url + currentMatch);

                    m = m.NextMatch();
                }
            }
            catch (WebException)
            {
                sitemap.Remove(url);
            }
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