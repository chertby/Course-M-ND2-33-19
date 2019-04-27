using System;
using Htp.ITnews.Data.Contracts.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Htp.ITnews.Data.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // cd Projects/Course-M-ND2-33-19/Skuratovich/src/ITnews/Htp.ITnews/Htp.ITnews.Data.EntityFramework/
        //
        // dotnet ef migrations add InitialCreate --startup-project ...ITnews/Htp.ITnews/Htp.ITnews.Web/Htp.ITnews.Web.csproj
        // dotnet ef migrations add InitialCreate
        //
        // dotnet ef database update
        // dotnet ef database update --startup-project /users/user/Projects/Course-M-ND2-33-19/Skuratovich/src/ITnews/Htp.ITnews/Htp.ITnews.Web/Htp.ITnews.Web.csproj

        public DbSet<News> News { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
