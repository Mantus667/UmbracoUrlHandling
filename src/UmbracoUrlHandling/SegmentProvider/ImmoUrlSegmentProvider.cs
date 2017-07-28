using System.Globalization;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Strings;

namespace UmbracoUrlHandling.SegmentProvider
{
	/// <summary>
	/// Url segment provider for real estates
	/// </summary>
	/// <seealso cref="Umbraco.Core.Strings.IUrlSegmentProvider" />
	public class ImmoUrlSegmentProvider : IUrlSegmentProvider
    {
        /// <summary>
        /// The provider.
        /// </summary>
        private readonly IUrlSegmentProvider _provider = new DefaultUrlSegmentProvider();

		/// <summary>
		/// Gets the default url segment for a specified content.
		/// </summary>
		/// <param name="content">The content.</param>
		/// <returns>
		/// The url segment.
		/// </returns>
		public string GetUrlSegment(IContentBase content)
        {
            return GetUrlSegment(content, CultureInfo.CurrentCulture);
        }

		/// <summary>
		/// Gets the url segment for a specified content and culture.
		/// </summary>
		/// <param name="content">The content.</param>
		/// <param name="culture">The culture.</param>
		/// <returns>
		/// The url segment.
		/// </returns>
		/// <remarks>
		/// This is for when Umbraco is capable of managing more than one url
		/// per content, in 1-to-1 multilingual configurations. Then there would be one
		/// url per culture.
		/// </remarks>
		public string GetUrlSegment(IContentBase content, CultureInfo culture)
        {
            if (content.GetContentType().Alias != "realEstate")
            {
                return null;
            }

            var segment = _provider.GetUrlSegment(content);
            return $"{content.GetValue("type")}-{content.GetValue<string>("city")}-{segment}".ToUrlSegment();
        }
    }
}