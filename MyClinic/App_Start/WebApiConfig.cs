using AutoMapper;
using MyClinic.Controllers;
using MyClinic.ViewModels;
using MyClinic.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MyClinic
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            /*config.Routes.MapHttpRoute(
            name: "ActionApi",
            routeTemplate: "api/patients/{action}/{id}",
            defaults: new { controller = "patients", action = "GetPatientByName"}
            );*/

            config.Routes.MapHttpRoute(
            name: "Patients",
            routeTemplate: "api/patients/{id}",
            defaults: new { controller = "Patients", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
            name: "PatientsByName",
            routeTemplate: "api/patients/{name}",
            defaults: new { controller = "Patients"}
            );

            

            config.Routes.MapHttpRoute(
            name: "Observations",
            routeTemplate: "api/patients/{patientid}/observations/{id}",
            defaults: new { controller = "Observations", id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            Mapper.Initialize(mapperConfig =>
                {
                    mapperConfig.CreateMap<PatientViewModel, PatientModel>().ReverseMap();
                    mapperConfig.CreateMap<ObservationViewModel, ObservationModel>().ReverseMap();

                });
        }
    }
}
