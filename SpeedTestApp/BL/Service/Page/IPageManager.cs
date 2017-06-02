using SpeedTestApp.Models;
using System.Collections.Generic;

namespace SpeedTestApp.BL.Service
{
    public interface IPageManager
    {
        void AddPages(IEnumerable<Page> page, Site site);
        Page GetPage(string url, Site site);
        Page GetPageById(int Id);
    }
}
