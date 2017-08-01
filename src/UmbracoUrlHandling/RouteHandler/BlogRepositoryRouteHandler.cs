using Umbraco.Web.Mvc;

namespace UmbracoUrlHandling.RouteHandler
{
    /// <summary>
    /// BlogPostRepositoryrouteHandler to handle routes to BlogPostRepository
    /// </summary>
    /// <seealso cref="Umbraco.Web.Mvc.UmbracoVirtualNodeByIdRouteHandler" />
    public class BlogRepositoryRouteHandler : UmbracoVirtualNodeByIdRouteHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlogRepositoryRouteHandler"/> class.
        /// </summary>
        /// <param name="realNodeId">The real node identifier.</param>
        public BlogRepositoryRouteHandler(int realNodeId) : base(realNodeId)
        {
        }
    }
}