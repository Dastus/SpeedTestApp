using System.Collections.Generic;
using System.Linq;
using SpeedTestApp.Models;
using SpeedTestApp.DAL.EF;

namespace SpeedTestApp.BL.Repository.EF
{
    public class EfPageRepository : IPageRepository
    {
        public void AddPages(IEnumerable<Page> pages, Site site)
        {
            using (SitePerformanceDBContext context = new SitePerformanceDBContext())
            {
                foreach (Page p in pages)
                {
                    p.PageId = site.Pages.Value.Where(x => x.PageURL == p.PageURL)
                        .Select(x => x.PageId).FirstOrDefault();

                    PageEntity pageEntity = (PageEntity)new PageEntity().ConvertFromApplicationModel(p);
                    pageEntity.SiteID = site.SiteId;                                   

                    if (pageEntity.ID == 0)
                    {
                        context.Pages.Add(pageEntity);

                        if (pageEntity.Measures.Count > 0)
                        {
                            foreach (var m in pageEntity.Measures)
                                context.Measures.Add(m);
                        }
                    }

                    if (pageEntity.ID > 0)
                    {
                        // Update Min and Max values
                        var dbPage = site.Pages.Value.Where(x => x.PageId == pageEntity.ID).FirstOrDefault();
                        var pageContext = context.Pages.Where(x => x.ID == pageEntity.ID).FirstOrDefault();

                        if (pageEntity.MaxResponse > dbPage.MaxResponse)                                
                            pageContext.MaxResponse = pageEntity.MaxResponse;
                        if (pageEntity.MinResponse < dbPage.MinResponse)
                            pageContext.MinResponse = pageEntity.MinResponse;   
                        //
                        if (pageEntity.Measures.Count > 0)
                        {
                            foreach (var m in pageEntity.Measures)
                            {
                                m.PageID = pageEntity.ID;
                                context.Measures.Add(m);
                            }                                
                        }
                    }
                }
                context.SaveChanges();
            }
        }

        public Page GetPage(string url, Site site)
        {
            using (SitePerformanceDBContext context = new SitePerformanceDBContext())
            {
                PageEntity pageEntity = context.Pages.AsNoTracking().Include("Measures")
                    .FirstOrDefault(e => e.Page == url && e.SiteID == site.SiteId);
                return (pageEntity == null) ? null : pageEntity.ConvertToApplicationModel();
            }
        }

        public Page GetPageById(int Id)
        {
            using (SitePerformanceDBContext context = new SitePerformanceDBContext())
            {
                PageEntity pageEntity = context.Pages.AsNoTracking().Include("Measures")
                    .FirstOrDefault(e => e.ID == Id);
                return (pageEntity == null) ? null : pageEntity.ConvertToApplicationModel();
            }
        }       
    }
}