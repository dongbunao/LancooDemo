//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Web.Http;
//using Microsoft.Owin.Security.OAuth;
//using Newtonsoft.Json.Serialization;

//namespace LancooDemo
//{
//    public static class WebApiConfig
//    {
//        public static void Register(HttpConfiguration config)
//        {
//            // Web API 配置和服务
//            // 将 Web API 配置为仅使用不记名令牌身份验证。
//            config.SuppressDefaultHostAuthentication();
//            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

//            // Web API 路由
//            config.MapHttpAttributeRoutes();

//            config.Routes.MapHttpRoute(
//                name: "DefaultApi",
//                routeTemplate: "api/{controller}/{id}",
//                defaults: new { id = RouteParameter.Optional }
//            );
//        }
//    }
//}
using Newtonsoft.Json.Converters;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;
//using WebApiContrib.Formatting.Jsonp;

namespace Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            //激活web api 的cors功能，是其能够跨域
            //config.EnableCors();


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            //包含Context和Version的web api 的路由
            config.Routes.MapHttpRoute(
                name: "DefaultContextVersion",
                routeTemplate: "api/{Context}/{Version}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            //不包含Action的路由
            config.Routes.MapHttpRoute(
                name: "NoAction",
                routeTemplate: "api/{Context}/{Version}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            ////添加jsonp的序列化器, 使接口接受返回jsonp格式的返回值
            ////config.AddJsonpFormatter();
            ////移除web api中的xml序列化器，使web api的返回值一直为JSON格式
            //config.Formatters.Remove(config.Formatters.XmlFormatter);
            ////修改JSON序列化器中的序列化时期的格式
            //config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter
            //{
            //    DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss"
            //});
        }
    }

    public class SessionableControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public SessionableControllerHandler(RouteData routeData)
            : base(routeData)
        {

        }
    }

    public class SessionStateRouteHandler : IRouteHandler
    {
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return new SessionableControllerHandler(requestContext.RouteData);
        }
    }
}
