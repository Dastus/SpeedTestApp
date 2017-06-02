using System;
using SpeedTestApp.Models;
using SpeedTestApp.BL.Repository;

namespace SpeedTestApp.BL.Service
{
    public class MeasureManager : IMeasureManager
    {
        private IMeasureRepository measureRepository;

        public MeasureManager(IMeasureRepository repository)
        {
            this.measureRepository = repository;
        }

        public void AddMeasure(Measure measure, Page page)
        {
            if (measure == null || page == null)
                throw new ArgumentNullException("Measure or Page is Null.");
            else
                measureRepository.AddMeasure(measure, page);
        }

    }
}