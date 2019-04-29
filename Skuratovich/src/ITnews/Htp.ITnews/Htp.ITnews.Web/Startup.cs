using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Htp.ITnews.Infrastructure;
using Htp.ITnews.Web.Utilities;
using Htp.ITnews.Web.Resources;
using System.Reflection;
using Microsoft.Extensions.Localization;

namespace Htp.ITnews.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDataAccessServices(Configuration.GetConnectionString("DefaultConnection"));
            services.AppDomainServices();
            services.ConfigureIdentity();
            services.ConfigureCookie();
            services.AddAutoMapper();

            services.ConfigureRequestLocalization();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddViewLocalization(options => options.ResourcesPath = "Resources")
                .AddDataAnnotationsLocalization(o =>
                {
                    var type = typeof(ViewResource);
                    var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
                    var factory = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                    var localizer = factory.Create("ViewResource", assemblyName.Name);
                    o.DataAnnotationLocalizerProvider = (t, f) => localizer;
                })
                .AddRazorPagesOptions(options => {
                    options.Conventions.Add(new CultureTemplateRouteModelConvention());
                });
            //.AddRazorPagesOptions(options =>
            //{
            //    options.Conventions.AddPageRoute("/News", "");
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
