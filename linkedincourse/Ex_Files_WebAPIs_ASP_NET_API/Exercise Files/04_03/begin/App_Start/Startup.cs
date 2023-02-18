using System;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using ExploreCalifornia.Config;
using ExploreCalifornia.Constraints;
using ExploreCalifornia.Filters;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

[assembly: OwinStartup(typeof(ExploreCalifornia.Startup))]
namespace ExploreCalifornia
{
    public class Startup
    {
        public static HttpConfiguration HttpConfiguration { get; set; } = new HttpConfiguration();

        public void Configuration(IAppBuilder app)
        {
            var config = Startup.HttpConfiguration;

            var json = config.Formatters.JsonFormatter.SerializerSettings;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            ConfigureWebApi(app, config);
        }


        private static void ConfigureWebApi(IAppBuilder app, HttpConfiguration config)
        {
            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("identity", typeof(IdConstraint));
            config.MapHttpAttributeRoutes(constraintResolver);
            
            config.Filters.Add(new DbUpdateExceptionFilterAttribute());
            
            config.Formatters.XmlFormatter.UseXmlSerializer = true;

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }
    }
}