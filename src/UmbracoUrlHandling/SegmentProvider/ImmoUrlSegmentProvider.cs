using System.Globalization;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Strings;

namespace UmbracoUrlHandling.SegmentProvider
{
    public class ImmoUrlSegmentProvider : IUrlSegmentProvider
    {
        /// <summary>
        /// The provider.
        /// </summary>
        private readonly IUrlSegmentProvider _provider = new DefaultUrlSegmentProvider();

        public string GetUrlSegment(IContentBase content)
        {
            return GetUrlSegment(content, CultureInfo.CurrentCulture);
        }

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