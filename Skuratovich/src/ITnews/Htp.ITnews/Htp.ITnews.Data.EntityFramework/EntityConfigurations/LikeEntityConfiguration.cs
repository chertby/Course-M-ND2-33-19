using Htp.ITnews.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Htp.ITnews.Data.EntityFramework.EntityConfigurations
{
    public class LikeEntityConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(x => new { x.AppUserId, x.CommentId });

            builder.HasOne(x => x.AppUser)
                .WithMany(x => x.Likes)
                .HasForeignKey(x => x.AppUserId);

            builder.HasOne(x => x.Comment)
                .WithMany(x => x.Likes)
                .HasForeignKey(x => x.CommentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
