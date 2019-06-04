using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Htp.ITnews.Infrastructure;
using Htp.ITnews.Web.Utilities;
using Htp.ITnews.Web.Resources;
using System.Reflection;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using Htp.ITnews.Web.Authorization.Requirements;
using Htp.ITnews.Web.Authorization.Handlers;
using Htp.ITnews.Web.Hubs;

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

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];

                });

            services.AddDataAccessServices(Configuration.GetConnectionString("DefaultConnection"));
            services.AppDomainServices(Configuration);
            services.ConfigureIdentity();
            services.ConfigureCookie();
            services.ConfigureAutoMapper();
           
            services.ConfigureRequestLocalization();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
                    //policy => policy.RequireRole("Administrator").RequireAssertion(context => context.User.GetUserId();
                    policy => policy.RequireRole("Administrator"));
                options.AddPolicy("RequireRole",
                    policy => policy.RequireRole("Administrator", "Writer", "Reader"));
                options.AddPolicy("EditPolicy", policy =>
                    policy.Requirements.Add(new EditRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, SameAuthorHandler>();
            services.AddSingleton<IAuthorizationHandler, AdministratorHandler>();
            services.AddSingleton<IAuthorizationHandler, SameUserHandler>();

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                         .RequireAuthenticatedUser()
                         .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddViewLocalization(options => options.ResourcesPath = "Resources")
                .AddModelBindingMessagesLocalizer(services)
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
                })
                .AddRazorPagesOptions(options =>
                {
                    //options.Conventions.AddPageRoute("/News", "");
                    options.Conventions.AuthorizeFolder("/Admin", "RequireAdministratorRole");
                    options.Conventions.AllowAnonymousToPage("/Index");
                    options.Conventions.AllowAnonymousToPage("/News/Index");
                    options.Conventions.AllowAnonymousToPage("/News/Details");
                    options.Conventions.AllowAnonymousToPage("/Users/Index");
                    options.Conventions.AllowAnonymousToPage("/Identity/Account/Login");
                    options.Conventions.AllowAnonymousToPage("/Identity/Account/Register");
                });

            services.AddSignalR();
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

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chatHub");
            });

            app.UseMvc();
        }
    }
}
