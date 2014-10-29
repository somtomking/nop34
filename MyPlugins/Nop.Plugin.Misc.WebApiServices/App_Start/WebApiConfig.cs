using Autofac;
using Autofac.Integration.WebApi;
using Nop.Core.Infrastructure;
 
using Nop.Plugin.Misc.WebApiServices.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.WebApiServices
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // We have to register services specifically for the API calls!
            //builder.RegisterType<CategoryService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            //Update existing, don't create a new container
            builder.Update(EngineContext.Current.ContainerManager.Container);

            //Feed the current container to the AutofacWebApiDependencyResolver
            var resolver = new AutofacWebApiDependencyResolver(EngineContext.Current.ContainerManager.Container);
            config.DependencyResolver = resolver;



            //exception logger 
            config.Services.Add(typeof(IExceptionLogger), new SimpleExceptionLogger());
            //exception handler
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        


            //we will get JSON by default, but it will still allow you to return XML if you pass text/xml as the request Accept header
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configuration.EnsureInitialized();
           　
        }
    }
}
