using Umbraco.Core.Models;

namespace UmbracoUrlHandling.ContentFinder
{
	/// <summary>
	/// Represents a found item from content finder. USed for caching
	/// </summary>
	public class ContentFinderItem
	{
		/// <summary>
		/// Gets or sets the template.
		/// </summary>
		/// <value>
		/// The template.
		/// </value>
		public string Template { get; set; }

		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		/// <value>
		/// The content.
		/// </value>
		public IPublishedContent Content { get; set; }
	}
}