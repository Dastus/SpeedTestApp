using SpeedTestApp.Models;
using SpeedTestApp.BL.Service;

namespace SpeedTestApp.DAL.EF
{
    public partial class PageEntity : IStorageModel<Page>
    {
        public Page ConvertToApplicationModel()
        {
            Page page = new Page {
            PageId = this.ID,
            SiteID = this.SiteID,
            PageURL = this.Page,
            MaxResponse = this.MaxResponse,
            MinResponse = this.MinResponse            
            };

            if (this.Measures.Count > 0)
            {
                foreach (var m in this.Measures)
                    page.Measures.Value.Add(m.ConvertToApplicationModel());
            }

            return page;
        }

        public IStorageModel<Page> ConvertFromApplicationModel(Page page)
        {
            if (page == null)
                return null;

            PageEntity pageEntity = new PageEntity {                
                Page = page.PageURL,
                ID = page.PageId,
                SiteID = page.SiteID,
                MaxResponse = page.MaxResponse,
                MinResponse = page.MinResponse
            };

            if (page.Measures.Value.Count > 0)
            {
                foreach (var m in page.Measures.Value)
                    pageEntity.Measures.Add((MeasureEntity)new MeasureEntity().ConvertFromApplicationModel(m));
            }

            return pageEntity;
        }
    }
}