using System.Reflection;
using Htp.ITnews.Web.Resources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Htp.ITnews.Web.Utilities
{
    public static class MvcOptionsExtension
    {
        /// <summary>
        /// localize ModelBinding messages, e.g. when user enters string value instead of number...
        /// these messages can't be localized like data attributes
        /// </summary>
        /// <param name="mvc"></param>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IMvcBuilder AddModelBindingMessagesLocalizer
            (this IMvcBuilder mvc, IServiceCollection services)
        {
            return mvc.AddMvcOptions(options =>
            {
                var type = typeof(ViewResource);
                var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
                var factory = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                var localizer = factory.Create("ViewResource", assemblyName.Name);

                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => localizer["A value is required."]);

                //options.ModelBindingMessageProvider
                //options.ModelBindingMessageProvider
                    //.SetAttemptedValueIsInvalidAccessor((x, y) => localizer["'{0}' is not valid value for '{0}' field", x, y]);

            });
        }
    }
}
