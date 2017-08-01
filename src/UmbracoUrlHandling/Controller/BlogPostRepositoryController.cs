using System.Linq;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using UmbracoUrlHandling.Models;

namespace UmbracoUrlHandling.Controller
{
    /// <summary>
    /// Route hijacking using controller to handle custom model building for templates
    /// </summary>
    /// <seealso cref="Umbraco.Web.Mvc.RenderMvcController" />
    public class BlogPostRepositoryController : RenderMvcController
    {
        /// <summary>
        /// Method to show tags overview template.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ActionResult Categories(RenderModel model)
        {
            var tags = Services.TagService.GetAllContentTags().ToList();
            var viewModel = new TagsOverviewViewModel(model.Content) {Tags = tags};
            return View("TagsOverview", viewModel);
        }

        /// <summary>
        /// Shows all content that was tagged with a specific category.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public ActionResult Category(RenderModel model, string category)
        {
            var tagsService = Services.TagService;
            var relatedContentTaggedEntities = tagsService.GetTaggedContentByTag(category);
            var relatedContent = Umbraco.TypedContent(relatedContentTaggedEntities.Select(x => x.EntityId));
            var viewModel = new CategoryContentViewModel(model.Content) { Category = category, RelatedContentForCategory = relatedContent};
            return View("RelatedContentForCategory", viewModel);
        }
    }
}