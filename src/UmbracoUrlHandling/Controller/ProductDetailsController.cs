using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace UmbracoUrlHandling.Controller
{
	/// <summary>
	/// Controller for Product page
	/// </summary>
	/// <seealso cref="Umbraco.Web.Mvc.RenderMvcController" />
	public class ProductDetailsController : RenderMvcController
	{
		/// <summary>
		/// The default action to render the front-end view
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[NonAction]
		public override ActionResult Index(RenderModel model)
		{
			return base.Index(model);
		}

		/// <summary>
		/// Indexes the specified model.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="sku">The sku.</param>
		/// <returns></returns>
		public ActionResult ProductDetails(RenderModel model, string sku)
		{
			ViewData["sku"] = sku;
			return View("ProductDetails", model);
		}
	}
}