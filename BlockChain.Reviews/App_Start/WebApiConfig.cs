using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace BlockChain.Reviews
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                 name: "GetStringAPI",
                 routeTemplate: "api/{controller}/{category}/{query}",
                 defaults: new { category = "getmovielist", query = RouteParameter.Optional }
             );
            config.Routes.MapHttpRoute(
                name: "GetblockListAPI",
                routeTemplate: "api/{controller}/{category}/{id}",
                defaults: new { category = "getblocklist", id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
               name: "GetblockQueryAPI",
               routeTemplate: "api/{controller}/{category}/{query}",
               defaults: new { category = "getblocklist", id = RouteParameter.Optional }
           );
            System.Net.Http.Headers.MediaTypeHeaderValue appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
