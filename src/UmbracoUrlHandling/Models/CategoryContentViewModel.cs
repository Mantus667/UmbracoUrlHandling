using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace UmbracoUrlHandling.Models
{
    /// <summary>
    /// ViewModel to render all content for aspecific category
    /// </summary>
    /// <seealso cref="Umbraco.Core.Models.PublishedContent.PublishedContentWrapped" />
    public class CategoryContentViewModel : PublishedContentWrapped
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryContentViewModel"/> class.
        /// </summary>
        /// <param name="content">The content to wrap and extend.</param>
        public CategoryContentViewModel(IPublishedContent content) : base(content)
        {
        }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the related content for category.
        /// </summary>
        /// <value>
        /// The related content for category.
        /// </value>
        public IEnumerable<IPublishedContent> RelatedContentForCategory { get; set; }
    }
}