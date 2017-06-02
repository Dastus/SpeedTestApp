using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpeedTestApp.Models;
using SpeedTestApp.DAL.EF;

namespace SpeedTestApp.BL.Repository.EF
{
    public class EfMeasureRepository : IMeasureRepository
    {
        public void AddMeasure(Measure measure, Page page)
        {            
            using (SitePerformanceDBContext context = new SitePerformanceDBContext())
            {
                MeasureEntity measureEntity = 
                    (MeasureEntity)new MeasureEntity().ConvertFromApplicationModel(measure);
                measureEntity.PageID = page.PageId;
                context.Measures.Add(measureEntity);
                context.SaveChanges();
            }
        }
    }
}