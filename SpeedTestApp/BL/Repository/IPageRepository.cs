using SpeedTestApp.Models;
using System.Collections.Generic;

namespace SpeedTestApp.BL.Repository
{
    public interface IPageRepository
    {
        void AddPages(IEnumerable<Page> pages, Site site);
        Page GetPage(string PageURL, Site site);
        Page GetPageById(int Id);
    }
}
