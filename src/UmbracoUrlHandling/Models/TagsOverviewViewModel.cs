using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace UmbracoUrlHandling.Models
{
    /// <summary>
    /// View model for tags overview of blog posts
    /// </summary>
    /// <seealso cref="Umbraco.Core.Models.PublishedContent.PublishedContentWrapped" />
    public class TagsOverviewViewModel : PublishedContentWrapped
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagsOverviewViewModel"/> class.
        /// </summary>
        /// <param name="content">The content to wrap and extend.</param>
        public TagsOverviewViewModel(IPublishedContent content) : base(content)
        {
        }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public IEnumerable<ITag> Tags { get; set; }
    }
}