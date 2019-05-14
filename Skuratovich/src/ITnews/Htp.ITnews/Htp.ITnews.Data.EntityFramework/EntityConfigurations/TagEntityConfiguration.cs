using System;
using Htp.ITnews.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Htp.ITnews.Data.EntityFramework.EntityConfigurations
{
    public class TagEntityConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Title).IsUnique();
            builder.HasData(
                new Tag { Id = Guid.NewGuid(), Title = "C" },
                new Tag { Id = Guid.NewGuid(), Title = "C++" },
                new Tag { Id = Guid.NewGuid(), Title = "C#" }
            );
        }
    }
}
