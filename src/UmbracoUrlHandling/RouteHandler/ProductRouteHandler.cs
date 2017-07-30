using Umbraco.Web.Mvc;

namespace UmbracoUrlHandling.RouteHandler
{
	/// <summary>
	/// Product Route Handler for Product Page
	/// </summary>
	/// <seealso cref="Umbraco.Web.Mvc.UmbracoVirtualNodeByIdRouteHandler" />
	public class ProductRouteHandler : UmbracoVirtualNodeByIdRouteHandler
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ProductRouteHandler"/> class.
		/// </summary>
		/// <param name="realNodeId">The real node identifier.</param>
		public ProductRouteHandler(int realNodeId) : base(realNodeId)
		{
		}
	}
}