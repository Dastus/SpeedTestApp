using SpeedTestApp.Models;
using SpeedTestApp.UI.ViewModels;

namespace SpeedTestApp.BL.Service
{
    public interface ISiteManager
    {
        void Add(Site site);
        Site GetSite(string url);
        MeasuresViewModel GetViewModel(Site site);
    }
}
