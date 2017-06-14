using System;
using SpeedTestApp.Models;
using SpeedTestApp.BL.Repository;
using System.Threading.Tasks;
using SpeedTestApp.UI.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SpeedTestApp.BL.Service
{
    public class SiteManager : ISiteManager
    {
        private ISiteRepository siteRepository;
        private IPageRepository pageRepository;

        public SiteManager(ISiteRepository siteRepository, IPageRepository pageRepository)
        {
            this.siteRepository = siteRepository;
            this.pageRepository = pageRepository;
        }

        public void Add(Site site)
        {             
            if (site == null)
                throw new ArgumentNullException(nameof(site));

            var dbSite = GetSite(site.Url);
            if (dbSite != null)
            {                
                pageRepository.AddPages(site.Pages.Value, dbSite);
            }                  
            else
                siteRepository.Add(site);   
        }
        
        public Site GetSite(string url)
        {
            return (url == null) ? null : siteRepository.GetSite(url);
        }

        public MeasuresViewModel GetViewModel(Site site)
        {
            if (site == null)
                throw new ArgumentNullException(nameof(site));

            Add(site);

            Site historicalSite = GetSite(site.Url);

            List<Page> historicalResults = historicalSite.Pages.Value.
                OrderByDescending(x => x.MaxResponse).ToList();

            List<Page> currentResults = site.Pages.Value.
                OrderByDescending(x => x.MaxResponse).ToList();            

            int max = currentResults.Max().MaxResponse;
                
            List<DisplayPageInfo> currentGraphResults = new List<DisplayPageInfo>();
            foreach (var p in currentResults)
            {
                int result = p.Measures.Value.FirstOrDefault().Result;
                currentGraphResults.Add(
                    new DisplayPageInfo {
                        URL = p.PageURL,
                        MeasureResult = result,
                        Percent = result * 100 / max
                    });
            }

            MeasuresViewModel vm = new MeasuresViewModel
            {
                Site = site.Url,
                HistoricalResults = historicalResults,                
                CurrentResultsGraph = currentGraphResults
            };

            return vm;
        }
    }
}