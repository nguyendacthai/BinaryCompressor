using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using ApiMultiPartFormData;

namespace Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var enableCorsAttribute = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(enableCorsAttribute);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
          
            config.Formatters.Add(new ApiMultipartFormDataFormatter());
        }
    }
}
