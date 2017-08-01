using System.Linq;
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

            //Make a route based on a node Id
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

            //Dynamic routes based on node type and node urls
            var blogRepositoryNodes = UmbracoContext.Current.ContentCache.GetByXPath("//BlogPostRepository").ToArray();

            foreach (var repository in blogRepositoryNodes)
            {
                var uri = repository.Url().TrimStart("/");
                var hash = uri.GetHashCode();

                RouteTable.Routes.MapUmbracoRoute(
                    $"blog_repository_categories_{hash}",
                    $"{uri.EnsureEndsWith('/')}categories/",
                    new
                    {
                        controller = "BlogPostRepository",
                        action = "Categories"
                    },
                    new BlogRepositoryRouteHandler(repository.Id));

                RouteTable.Routes.MapUmbracoRoute(
                    $"blog_repository_category_{hash}",
                    $"{uri.EnsureEndsWith('/')}category/{{category}}",
                    new
                    {
                        controller = "BlogPostRepository",
                        action = "Category",
                        category = UrlParameter.Optional
                    },
                    new BlogRepositoryRouteHandler(repository.Id));
            }
        }
    }
}