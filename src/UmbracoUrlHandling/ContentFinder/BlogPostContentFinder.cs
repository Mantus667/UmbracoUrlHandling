using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Web;
using Umbraco.Web.Routing;

namespace UmbracoUrlHandling.ContentFinder
{
	/// <summary>
	/// Custom content finder for blog posts
	/// </summary>
	/// <seealso cref="Umbraco.Web.Routing.IContentFinder" />
	public class BlogPostContentFinder : IContentFinder
	{
		/// <summary>
		/// Tries to find and assign an Umbraco document to a <c>PublishedContentRequest</c>.
		/// </summary>
		/// <param name="contentRequest">The <c>PublishedContentRequest</c>.</param>
		/// <returns>
		/// A value indicating whether an Umbraco document was found and assigned.
		/// </returns>
		/// <remarks>
		/// Optionally, can also assign the template or anything else on the document request, although that is not required.
		/// </remarks>
		public bool TryFindContent(PublishedContentRequest contentRequest)
		{
			if (contentRequest == null) return false;

			try
			{
				//Get the current url.
				var url = contentRequest.Uri.AbsoluteUri;

				//Get the news nodes that are already cached.
				var cachedBlogPostNodes = (Dictionary<string, ContentFinderItem>)HttpContext.Current.Cache["CachedBlogPostNodes"];
				if (cachedBlogPostNodes != null)
				{
					//Check if the current url already has a news item.
					if (cachedBlogPostNodes.ContainsKey(url))
					{
						//If the current url already has a node use that so the rest of the code doesn't need to run again.
						var contentFinderItem = cachedBlogPostNodes[url];
						contentRequest.PublishedContent = contentFinderItem.Content;
						contentRequest.TrySetTemplate(contentFinderItem.Template);
						return true;
					}
				}

				//Split the url segments.
				var path = contentRequest.Uri.GetAbsolutePathDecoded();
				var parts = path.Split(new[] { '/' }, System.StringSplitOptions.RemoveEmptyEntries);

				//Get all the root nodes.
				var rootNodes = contentRequest.RoutingContext.UmbracoContext.ContentCache.GetAtRoot();

				//Find the news item that matches the last segment in the url.
				var newsItem = rootNodes.DescendantsOrSelf("BlogPost").FirstOrDefault(x => x.UrlName == parts.Last());

				if (newsItem == null) return false;

				//Get the news item template.
				var template = ApplicationContext.Current.Services.FileService.GetTemplate(newsItem.TemplateId);

				if (template != null)
				{
					//Store the fields in the ContentFinderItem-object.
					var contentFinderItem = new ContentFinderItem()
					{
						Template = template.Alias,
						Content = newsItem
					};

					//If the correct node is found display that node.
					contentRequest.PublishedContent = contentFinderItem.Content;
					contentRequest.TrySetTemplate(contentFinderItem.Template);

					if (cachedBlogPostNodes != null)
					{
						//Add the new ContentFinderItem-object to the cache.
						cachedBlogPostNodes.Add(url, contentFinderItem);
					}
					else
					{
						//Create a new dictionary and store it in the cache.
						cachedBlogPostNodes = new Dictionary<string, ContentFinderItem>()
						{
							{
								url, contentFinderItem
							}
						};
						HttpContext.Current.Cache.Add("CachedBlogPostNodes",
							cachedBlogPostNodes,
							null,
							DateTime.Now.AddDays(1),
							System.Web.Caching.Cache.NoSlidingExpiration,
							System.Web.Caching.CacheItemPriority.High,
							null);
					}
				}
			}
			catch (Exception ex)
			{
				LogHelper.Error<BlogPostContentFinder>("Error during matching url to blog post", ex);
			}
			return contentRequest.PublishedContent != null;
		}
	}
}