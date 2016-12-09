//using AutoMapper;

using AutoMapper;
using deprosa.Service.Automapper;
using deprosa.API;
using deprosa.WebApi.Authenticator;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace deprosa.API
{
    using System.Web.Http;
    using Owin;

    public class Startup
    {
        private MapperConfiguration MapperConfiguration { get; set; }

        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            // Configure Web API Routes:
            // - Enable Attribute Mapping
            // - Enable Default routes at /api.
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",

                defaults: new { id = RouteParameter.Optional }
            );

            httpConfiguration.MessageHandlers.Add(new AuthenticationMessageHandler());
            app.UseWebApi(httpConfiguration);

            // Make ./public the default root of the static files in our Web Application.
            //app.UseFileServer(new FileServerOptions
            //{
            //    RequestPath = new PathString(string.Empty),
            //    FileSystem = new PhysicalFileSystem("./public"),
            //    EnableDirectoryBrowsing = true,
            //});
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DeprosaAutoMapper());
            });
        }
    }
}
