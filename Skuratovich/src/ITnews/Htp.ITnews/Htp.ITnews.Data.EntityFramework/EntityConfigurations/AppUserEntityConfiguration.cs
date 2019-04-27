using Htp.ITnews.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Htp.ITnews.Data.EntityFramework.EntityConfigurations
{
    public class AppUserEntityConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.FirstName);
            builder.Property(x => x.LastName);
            builder.HasMany(x => x.News).WithOne(x => x.Author);
            builder.HasMany(x => x.UpdatedNews).WithOne(x => x.UpdatedBy);
            builder.HasMany(x => x.Comments).WithOne(x => x.Author);
        }
    }
}
