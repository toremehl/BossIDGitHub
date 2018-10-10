using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server;

#pragma warning disable 1591

namespace BossIDWS.Vendor.REST
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Uncomment line below to start using API_KEY autentication
            // Requests wil have to set the request header API_KEY to a valid key - see VendorAPIKey in web.config for valid key
            //GlobalConfiguration.Configuration.MessageHandlers.Add(new APIKeyHandler());

            GlobalConfiguration.Configuration.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));
        }
    }
}