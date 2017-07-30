using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Core.Strings;
using Umbraco.Web;
using Umbraco.Web.Routing;
using UmbracoUrlHandling.ContentFinder;
using UmbracoUrlHandling.RouteHandler;
using UmbracoUrlHandling.SegmentProvider;
using UmbracoUrlHandling.UrlProvider;

namespace UmbracoUrlHandling
{
    public class Startup : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            UrlSegmentProviderResolver.Current.InsertTypeBefore<DefaultUrlSegmentProvider, ImmoUrlSegmentProvider>();

	        UrlProviderResolver.Current.InsertTypeBefore<DefaultUrlProvider, BlogPostUrlProvider>();

	        ContentFinderResolver.Current.InsertTypeBefore<ContentFinderByNotFoundHandlers, BlogPostContentFinder>();

	        RouteTable.Routes.MapUmbracoRoute(
		        "ProductRoute",
		        "Products/{sku}",
		        new
		        {
			        controller = "ProductDetails",
					action = "ProductDetails",
			        sku = UrlParameter.Optional
		        },
		        new ProductRouteHandler(1100)
	        );
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
	        ContentService.Published += (sender, args) => HttpContext.Current.Cache.Remove("CachedBlogPostNodes");
        }
    }
}