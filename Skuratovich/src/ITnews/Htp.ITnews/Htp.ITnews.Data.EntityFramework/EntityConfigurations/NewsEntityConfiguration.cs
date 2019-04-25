using Htp.ITnews.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Htp.ITnews.Data.EntityFramework.EntityConfigurations
{
    public class NewsEntityConfiguration : IEntityTypeConfiguration<News>
    {

        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.Updated).IsRequired();
            builder.Property(x => x.Rating).HasDefaultValue(0);
            builder.HasOne(x => x.Category).WithMany(x => x.News).IsRequired();
            builder.HasMany(x => x.Comments).WithOne(x => x.News).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
