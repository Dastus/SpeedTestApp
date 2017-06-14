using SpeedTestApp.Models;

namespace SpeedTestApp.BL.Repository.Memory
{
    public class MemorySiteRepository : ISiteRepository
    {
        public void Add(Site site)
        {
            Site stored = GetSite(site.Url);
            if (stored == null)
            {
                foreach (var p in site.Pages.Value)
                {
                    p.PageId = AppCache.Instance.GetId();
                    AppCache.Instance.UpdateSitesDict(p.PageId, site.Url);
                }                    
                AppCache.Instance.Add(site);
            }                
            else
            {
                MemoryPageRepository pageRepository = new MemoryPageRepository();
                pageRepository.AddPages(site.Pages.Value, stored);
            }
        }

        public Site GetSite(string url)
        {
            return AppCache.Instance.GetValue(url);
        }
    }
}