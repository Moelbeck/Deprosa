using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using deprosa.Service.Automapper;

namespace deprosa.Web

{
    public class MvcApplication : System.Web.HttpApplication
    {
        private MapperConfiguration MapperConfiguration { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DeprosaAutoMapper());
            });
        }
    }
}
