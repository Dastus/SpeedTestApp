using SpeedTestApp.Models;

namespace SpeedTestApp.BL.Measurers
{
    public interface IMeasurer
    {
        Site MeasureResponse(string url);
    }
}
