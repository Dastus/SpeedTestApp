using SpeedTestApp.Models;
using SpeedTestApp.BL.Service;

namespace SpeedTestApp.DAL.EF
{
    public partial class MeasureEntity : IStorageModel<Measure>
    {
        public Measure ConvertToApplicationModel()
        {
            Measure measure = new Measure
            {
                MeasureId = this.ID,
                PageID = this.PageID,
                MeasureTime = this.MeasureTime,
                Result = this.Result
            };

            return measure;
        }

        public IStorageModel<Measure> ConvertFromApplicationModel(Measure measure)
        {
            if (measure == null)
                return null;

            MeasureEntity measureEntity = new MeasureEntity {
                PageID = measure.PageID,
                MeasureTime = measure.MeasureTime,
                Result = measure.Result
            };

            return measureEntity;
        }
    }
}