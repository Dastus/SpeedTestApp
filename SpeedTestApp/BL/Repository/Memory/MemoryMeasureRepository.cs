using System;
using SpeedTestApp.Models;

namespace SpeedTestApp.BL.Repository.Memory
{
    public class MemoryMeasureRepository : IMeasureRepository
    {
        /// <summary>
        /// Method isn't used in current version of application, so not implemented
        /// </summary>
        /// <param name="measure"></param>
        /// <param name="page"></param>
        public void AddMeasure(Measure measure, Page page)
        {
            throw new NotImplementedException();
        }
    }
}