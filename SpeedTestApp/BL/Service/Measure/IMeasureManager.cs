using SpeedTestApp.Models;

namespace SpeedTestApp.BL.Service
{
    public interface IMeasureManager
    {
        void AddMeasure(Measure measure, Page page);
    }
}
