using System;
using System.IO;
using AutoMapper;
using Htp.ITnews.Data.Contracts;
using Htp.ITnews.Data.Contracts.Entities;
using Htp.ITnews.Data.EntityFramework;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Htp.ITnews.Domain.Services;
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
                .UseSqlite($"Filename={wanted_path}/{connectionString}"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INewsRepository, NewsRepository>();
        }

        public static void AppDomainServices(this IServiceCollection services)
        {
            services.AddScoped<INewsService, NewsService>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            //services.AddDefaultIdentity<UserViewModel>()
            //services.AddIdentity<UserViewModel, IdentityRole<Guid>>()
            services.AddIdentity<UserViewModel, IdentityRole<Guid>>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

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

        public static void ConfigureCookie(this IServiceCollection services)
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

        public static void AddAutoMapper(this IServiceCollection services)
        {
            ////// Auto Mapper Configurations
            //var mappingConfig = new MapperConfiguration(cfg =>
            //{
            //    // Add all profiles in current assembly
            //    cfg.AddMaps(typeof(ServiceCollectionExtensions).Assembly);
            //});

            //IMapper mapper = mappingConfig.CreateMapper();
            //services.AddSingleton(mapper);

            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        }

        //FluentValidation.AspNetCore
    }
}
