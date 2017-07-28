using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Web.Routing;

namespace UmbracoUrlHandling.UrlProvider
{
	/// <summary>
	/// BlogPost url provider for custom urls
	/// </summary>
	/// <seealso cref="Umbraco.Web.Routing.IUrlProvider" />
	public class BlogPostUrlProvider : IUrlProvider
	{
		/// <summary>
		/// Gets the nice url of a published content.
		/// </summary>
		/// <param name="umbracoContext">The Umbraco context.</param>
		/// <param name="id">The published content id.</param>
		/// <param name="current">The current absolute url.</param>
		/// <param name="mode">The url mode.</param>
		/// <returns>
		/// The url for the published content.
		/// </returns>
		/// <remarks>
		/// <para>The url is absolute or relative depending on <c>mode</c> and on <c>current</c>.</para>
		/// <para>If the provider is unable to provide a url, it should return <c>null</c>.</para>
		/// </remarks>
		public string GetUrl(UmbracoContext umbracoContext, int id, Uri current, UrlProviderMode mode)
		{
			var content = umbracoContext.ContentCache.GetById(id);
			if (content == null || content.DocumentTypeAlias != "BlogPost" || content.Parent == null) return null;

			var date = content.CreateDate;
			//This will add the selected date before the node name.
			//For example /news/item1/ becomes /news/28-07-2014/item1/.
			var url = content.Parent.Url;
			url.EnsureEndsWith('/');
			return $"{url}{date.ToString("dd-MM-yyyy").EnsureEndsWith('/')}{content.UrlName.EnsureEndsWith('/')}";
		}

		/// <summary>
		/// Gets the other urls of a published content.
		/// </summary>
		/// <param name="umbracoContext">The Umbraco context.</param>
		/// <param name="id">The published content id.</param>
		/// <param name="current">The current absolute url.</param>
		/// <returns>
		/// The other urls for the published content.
		/// </returns>
		/// <remarks>
		/// Other urls are those that <c>GetUrl</c> would not return in the current context, but would be valid
		/// urls for the node in other contexts (different domain for current request, umbracoUrlAlias...).
		/// </remarks>
		public IEnumerable<string> GetOtherUrls(UmbracoContext umbracoContext, int id, Uri current)
		{
			return Enumerable.Empty<string>();
		}
	}
}