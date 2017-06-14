using System.Collections.Generic;
using System.Linq;
using SpeedTestApp.Models;

namespace SpeedTestApp.BL.Repository.Memory
{
    public class MemoryPageRepository : IPageRepository
    {
        public void AddPages(IEnumerable<Page> pages, Site site)
        {
            Site stored = site;

            IEnumerable<string> commonUrls = stored.Pages.Value.Select(x => x.PageURL);
            IEnumerable<Page> newPages = pages.Where(x => !commonUrls.Contains(x.PageURL));
            IEnumerable<Page> existingPages = pages.Where(x => commonUrls.Contains(x.PageURL));

            foreach (var p in newPages)
            {
                p.PageId = AppCache.Instance.GetId();
                AppCache.Instance.UpdateSitesDict(p.PageId, site.Url);
                stored.Pages.Value.Add(p);
            }

            foreach (var p in existingPages)
            {
                Page storedPage = stored.Pages.Value.Where(x => x.PageURL == p.PageURL).FirstOrDefault();

                if (p.MaxResponse > storedPage.MaxResponse)
                    stored.Pages.Value.Where(x => x.PageURL == p.PageURL).FirstOrDefault()
                        .MaxResponse = p.MaxResponse;

                if (p.MinResponse < storedPage.MinResponse)
                    stored.Pages.Value.Where(x => x.PageURL == p.PageURL).FirstOrDefault()
                        .MinResponse = p.MinResponse;

                stored.Pages.Value.Where(x => x.PageURL == p.PageURL).FirstOrDefault()
                    .Measures.Value.AddRange(p.Measures.Value);
            }

            AppCache.Instance.Update(stored);    
        }

        public Page GetPageById(int id)
        {
            Site stored = AppCache.Instance.GetStoredSiteByPageId(id);
            Page page = stored.Pages.Value.Where(x=> x.PageId == id).FirstOrDefault();
            return (page == null) ? null : page;
        }

        public Page GetPage(string url, Site site)
        {
            Site stored = AppCache.Instance.GetValue(url);
            Page page = stored.Pages.Value.Where(x => x.PageURL == url).FirstOrDefault();
            return (page == null) ? null : page;
        }

    }
}