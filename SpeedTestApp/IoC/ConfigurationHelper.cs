using StructureMap;
using SpeedTestApp.Models;
using SpeedTestApp.DAL.EF;
using SpeedTestApp.BL.Repository;
using SpeedTestApp.BL.Repository.EF;
using SpeedTestApp.BL.Service;
using SpeedTestApp.BL.Measurers;

namespace SpeedTestApp.IoC
{
    public class ConfigurationHelper
    {
        public static void ConfigureDependencies(ConfigurationExpression temp)
        {
            temp.For<IMeasurer>().Use<Measurer>();
            temp.For<ISiteRepository>().Use<EfSiteRepository>();
            temp.For<IPageRepository>().Use<EfPageRepository>();
            temp.For<IMeasureRepository>().Use<EfMeasureRepository>();
            temp.For<IPageManager>().Use<PageManager>();
            temp.For<ISiteManager>().Use<SiteManager>();
            temp.For<IMeasureManager>().Use<MeasureManager>();
        }
    }
}