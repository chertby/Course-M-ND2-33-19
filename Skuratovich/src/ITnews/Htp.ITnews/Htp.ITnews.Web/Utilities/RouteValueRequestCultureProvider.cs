using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Htp.ITnews.Web.Utilities
{
    public class RouteValueRequestCultureProvider : IRequestCultureProvider
    {
        private readonly CultureInfo[] cultures;

        public RouteValueRequestCultureProvider(CultureInfo[] cultures)
        {
            this.cultures = cultures;
        }

        /// <summary>
        /// get {culture} route value from path string, 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns>ProviderCultureResult depends on path {culture} route parameter, or default culture</returns>
        public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var defaultCulture = "en";

            var path = httpContext.Request.Path;

            if (string.IsNullOrWhiteSpace(path))
            {
                return Task.FromResult(new ProviderCultureResult(defaultCulture));
            }

            var routeValues = httpContext.Request.Path.Value.Split('/');
            if (routeValues.Count() <= 1)
            {
                return Task.FromResult(new ProviderCultureResult(defaultCulture));
            }

            if (!this.cultures.Any(x => x.Name.ToLower() == routeValues[1].ToLower()))
            {
                return Task.FromResult(new ProviderCultureResult(defaultCulture));
            }

            return Task.FromResult(new ProviderCultureResult(routeValues[1]));
        }
    }
}
