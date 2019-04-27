using Htp.ITnews.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Htp.ITnews.Data.EntityFramework.EntityConfigurations
{
    public class RatingEntityConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(x => new { x.AppUserId, x.NewsId });

            builder.HasOne(x => x.AppUser)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.AppUserId);

            builder.HasOne(x => x.News)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.NewsId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
