using System.Web.Http;
using BabyDiary.Helpers;

namespace BabyDiary
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new ValidateModelAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }

            );

            // WebAPI when dealing with JSON & JavaScript!
            // Setup json serialization to serialize classes to camel (std. Json format)
//            var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
//            formatter.SerializerSettings.ContractResolver =
//                new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }
    }
}