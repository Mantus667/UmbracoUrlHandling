using Umbraco.Core;
using Umbraco.Core.Strings;
using UmbracoUrlHandling.SegmentProvider;

namespace UmbracoUrlHandling
{
    public class Startup : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            UrlSegmentProviderResolver.Current.InsertType<ImmoUrlSegmentProvider>(0);
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }
    }
}