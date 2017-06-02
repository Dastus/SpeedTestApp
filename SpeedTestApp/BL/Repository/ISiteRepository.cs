using SpeedTestApp.Models;

namespace SpeedTestApp.BL.Repository
{
    public interface ISiteRepository
    {
        void Add(Site site);
        Site GetSite(string url);
    }
}
