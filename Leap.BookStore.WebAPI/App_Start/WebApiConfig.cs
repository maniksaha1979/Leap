using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Validation.Providers;

namespace Leap.BookStore.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


            // Convention-based routing.
            config.Routes.MapHttpRoute(
                 name: "DefaultApiWithAction",
                 routeTemplate: "api/{controller}/{action}/{id}",
                 defaults: new { id = RouteParameter.Optional }
              );

            // Remove the XML Formatter
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            // Remove the OVER THE TOP http model validation
            config.Services.RemoveAll(
                typeof(System.Web.Http.Validation.ModelValidatorProvider),
                v => v is InvalidModelValidatorProvider);

            config.Formatters.JsonFormatter
                .SerializerSettings
                .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
