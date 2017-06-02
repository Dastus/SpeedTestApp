using SpeedTestApp.Models;
using SpeedTestApp.BL.Service;

namespace SpeedTestApp.DAL.EF
{
    public partial class SiteEntity : IStorageModel<Site>
    {
        public Site ConvertToApplicationModel()
        {
            Site site = new Site {
                SiteId = this.ID,
                Url = this.URL
            };

            if (this.Pages.Count > 0)
            {
                foreach (var p in this.Pages)
                    site.Pages.Value.Add(p.ConvertToApplicationModel());
            }

            return site;
        }

        public IStorageModel<Site> ConvertFromApplicationModel(Site site)
        {
            if (site == null)
                return null;

            SiteEntity siteEntity = new SiteEntity {
            URL = site.Url
            };

            if (site.Pages.Value.Count > 0)
            {
                foreach (var p in site.Pages.Value)
                {                    
                    siteEntity.Pages.Add((PageEntity)new PageEntity().ConvertFromApplicationModel(p));
                }                
            }

            return siteEntity;
        }
    }
}