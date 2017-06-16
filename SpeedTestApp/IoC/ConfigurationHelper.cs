using StructureMap;
using SpeedTestApp.Models;
using SpeedTestApp.DAL.EF;
using SpeedTestApp.BL.Repository;
using SpeedTestApp.BL.Service.CustomWebClient;
using SpeedTestApp.BL.Repository.EF;
using SpeedTestApp.BL.Repository.Memory;
using SpeedTestApp.BL.Service;

namespace SpeedTestApp.IoC
{
    public class ConfigurationHelper
    {
        public static void ConfigureDependencies(ConfigurationExpression temp)
        {
            /*           
            temp.For<ISiteRepository>().Use<EfSiteRepository>();
            temp.For<IPageRepository>().Use<EfPageRepository>();
            temp.For<IMeasureRepository>().Use<EfMeasureRepository>();
            */
            temp.For<ISiteRepository>().Use<MemorySiteRepository>();
            temp.For<IPageRepository>().Use<MemoryPageRepository>();
            temp.For<IMeasureRepository>().Use<MemoryMeasureRepository>();

            temp.For<IPageManager>().Use<PageManager>();
            temp.For<ISiteManager>().Use<SiteManager>();
            temp.For<IMeasureManager>().Use<MeasureManager>();            
        }
    }
}