using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Nhom7_WebsiteClothes
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Web API mặc định trả về dạng XML. Remove format XML
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            //Config.Formatters.Remove(config.Formatters.XmlFormatter);

            //Now set the serializer setting for JsonFormatter to Indented to get json Formatted data
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //New CamelCasePropertyNamesContractResolver();
            //Format converting data
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            //Newtonsoft.Json.PreserveReferencesHandling.Objects;
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
