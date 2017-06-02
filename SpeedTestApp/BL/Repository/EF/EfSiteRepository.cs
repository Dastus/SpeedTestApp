using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpeedTestApp.Models;
using SpeedTestApp.DAL.EF;

namespace SpeedTestApp.BL.Repository.EF
{
    public class EfSiteRepository: ISiteRepository
    {
        public void Add(Site site)
        {
            using (SitePerformanceDBContext context = new SitePerformanceDBContext())
            {
                SiteEntity siteEntity = (SiteEntity)new SiteEntity().ConvertFromApplicationModel(site);

                context.Sites.Add(siteEntity);

                if (siteEntity.Pages.Count > 0)
                {
                    foreach (PageEntity p in siteEntity.Pages)
                    {
                        context.Pages.Add(p);

                        if (p.Measures.Count > 0)
                        {
                            foreach (MeasureEntity m in p.Measures)
                                context.Measures.Add(m);
                        }
                    }
                }                
                
                context.SaveChanges();
            }
        }

        public Site GetSite(string url)
        {
            using (SitePerformanceDBContext context = new SitePerformanceDBContext())
            {
                SiteEntity siteEntity = context.Sites.AsNoTracking().Include("Pages").
                    Include("Pages.Measures").FirstOrDefault(e => e.URL == url);
                return (siteEntity == null) ? null : siteEntity.ConvertToApplicationModel();
            }
        }
    }
}