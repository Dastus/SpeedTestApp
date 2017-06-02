using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SpeedTestApp.IoC;

namespace SpeedTestApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Bootstrapper.ConfigureStructureMap(ConfigurationHelper.ConfigureDependencies);
        }
    }
}
