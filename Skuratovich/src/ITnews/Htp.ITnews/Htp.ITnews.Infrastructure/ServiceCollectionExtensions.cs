using System;
using System.IO;
using Htp.ITnews.Data.Contracts.Entities;
using Htp.ITnews.Data.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Htp.ITnews.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataAccessServices(this IServiceCollection services, string connectionString)
        {
            string wanted_path = Path.GetDirectoryName(Directory.GetCurrentDirectory());

            services.AddDbContext<ApplicationDbContext>(options => options
                .UseSqlite($"Filename={wanted_path}/{connectionString}"))
                .AddDefaultIdentity<AppUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        public static void ConfigureIdentity(this IServiceCollection services, string connectionString)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-2.2&tabs=visual-studio

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                // SignIn settings.
                //options.SignIn.RequireConfirmedEmail = true;
                // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?view=aspnetcore-2.2&tabs=visual-studio
            });
        }

        public static void ConfigureCookie(this IServiceCollection services, string connectionString)
        {
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
        }

        //FluentValidation.AspNetCore
    }
}
