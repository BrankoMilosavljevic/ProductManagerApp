﻿using System.Web.Http;
using ProductManagerApp.Contract;
using ProductManagerApp.Domain;

namespace ProductManagerApp
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

            AutoMapper.Mapper.Initialize(mapperConfig =>
            {
                mapperConfig.CreateMap<ProductContract, Product>();
            });
        }
    }
}
