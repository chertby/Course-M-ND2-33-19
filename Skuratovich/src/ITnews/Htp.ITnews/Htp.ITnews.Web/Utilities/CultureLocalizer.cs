using System.Reflection;
using Htp.ITnews.Web.Resources;
using Microsoft.Extensions.Localization;

namespace Htp.ITnews.Web.Utilities
{
    public class CultureLocalizer
    {
        private readonly IStringLocalizer localizer;
        public CultureLocalizer(IStringLocalizerFactory factory)
        {
            var type = typeof(ViewResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            this.localizer = factory.Create("ViewResource", assemblyName.Name);
        }

        // if we have formatted string we can provide arguments         
        // e.g.: @Localizer.Text("Hello {0}", User.Name)
        public LocalizedString Text(string key, params string[] arguments)
        {
            return arguments == null
                ? localizer[key]
                : localizer[key, arguments];
        }
    }
}
