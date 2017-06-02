using SpeedTestApp.Models;

namespace SpeedTestApp.BL.Repository
{
    public interface IMeasureRepository
    {
        void AddMeasure(Measure measure, Page page);
    }
}
