using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Htp.ITnews.Web.Utilities
{
    public static class LocalizationExtension
    {
        /// <summary>
        /// localize request according to {culture} route value.
        /// define supported cultures list, 
        /// define default culture for fallback,
        /// customize culture info e.g.: dat time format, digit shape,...
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRequestLocalization(this IServiceCollection services)
        {
            var cultures = new CultureInfo[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru"),
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = cultures.OrderBy(x => x.EnglishName).ToList();
                options.SupportedUICultures = cultures.OrderBy(x => x.EnglishName).ToList();

                // add RouteValueRequestCultureProvider to the beginning of the providers list. 
                //options.RequestCultureProviders.Insert(0,
                    //new RouteValueRequestCultureProvider(cultures));
            });

            //services.AddSingleton<CultureLocalizer>();
        }
    }
}
