using System.Web;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Core.Strings;
using Umbraco.Web.Routing;
using UmbracoUrlHandling.ContentFinder;
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
		}

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
	        ContentService.Published += (sender, args) => HttpContext.Current.Cache.Remove("CachedBlogPostNodes");
        }
    }
}