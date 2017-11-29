using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace LancooDemo.Interface
{
    /// <summary>
    /// <para>这个类用于自定义web api的Controller.</para>
    /// <para>在路由时，则匹配相应的命名空间.</para>
    /// </summary>
    public class VersionHttpControllerSelector : DefaultHttpControllerSelector
    {
        private const string VersionKey = "version";
        private const string ControllerKey = "controller";

        private readonly HttpConfiguration _configuration;
        private readonly Lazy<ILookup<string, Type>> _apiControllerTypes;

        private ILookup<string, Type> ApiControllerTypes
        {
            get { return this._apiControllerTypes.Value; }
        }

        public VersionHttpControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
            this._configuration = configuration;
            this._apiControllerTypes = new Lazy<ILookup<string, Type>>(this.GetApiControllerTypes);
        }

        private ILookup<string, Type> GetApiControllerTypes()
        {
            IAssembliesResolver assembliesResolver = this._configuration.Services.GetAssembliesResolver();
            return this._configuration.Services.GetHttpControllerTypeResolver()
                .GetControllerTypes(assembliesResolver)
                .ToLookup(t => t.Name.ToLower().Substring(0, t.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length));
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            HttpControllerDescriptor des = null;
            string controllerName = this.GetControllerName(request);
            if (!string.IsNullOrWhiteSpace(controllerName))
            {
                var groups = this.ApiControllerTypes[controllerName.ToLower()];
                if (groups != null && groups.Any())
                {
                    string endString;
                    var routeDic = request.GetRouteData().Values;
                    if (routeDic.Count > 1)
                    {
                        StringBuilder tmp = new StringBuilder();
                        foreach (var key in routeDic.Keys)
                        {
                            tmp.Append('.');
                            tmp.Append(routeDic[key]);
                            if (key.Equals(DefaultHttpControllerSelector.ControllerSuffix, StringComparison.CurrentCultureIgnoreCase))
                            {
                                break;
                            }
                        }
                        tmp.Append(DefaultHttpControllerSelector.ControllerSuffix);
                        endString = tmp.ToString();
                    }
                    else
                    {
                        endString = string.Format(".{0}{1}", controllerName, DefaultHttpControllerSelector.ControllerSuffix);
                    }

                    var type = groups.Where(t => t.FullName.EndsWith(endString, StringComparison.CurrentCultureIgnoreCase))
                        .OrderBy(t => t.FullName.Count(s => s == '.')).FirstOrDefault();
                    if (type != null)
                        des = new HttpControllerDescriptor(this._configuration, controllerName, type);
                }
            }
            if (des == null)
                throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.NotFound,
                    string.Format("No route providing a controller name was found to math request URI '{0}'", request.RequestUri)));
            return des;

        }
    }
}