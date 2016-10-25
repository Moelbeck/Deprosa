using AutoMapper;
using biz2biz.Service.Automapper;
using Microsoft.Owin;
using salvir.API;
using salvir.WebApi.Authenticator;

[assembly: OwinStartup(typeof(Startup))]
namespace salvir.API
{
    using System.Web.Http;
    using Microsoft.Owin;
    using Microsoft.Owin.Extensions;
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;
    using Owin;

    public class Startup
    {
        private MapperConfiguration _mapperConfiguration { get; set; }

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
            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString(string.Empty),
                FileSystem = new PhysicalFileSystem("./public"),
                EnableDirectoryBrowsing = true,
            });
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BzaleAutoMapper());
            });
        }
    }
}
