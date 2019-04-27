using System;
using Htp.ITnews.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Htp.ITnews.Data.EntityFramework.EntityConfigurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired();
            builder.HasData(
                new Category { Id = Guid.NewGuid(), Title = "Java" },
                new Category { Id = Guid.NewGuid(), Title = "C#" },
                new Category { Id = Guid.NewGuid(), Title = "C++" },
                new Category { Id = Guid.NewGuid(), Title = "Algorithms" },
                new Category { Id = Guid.NewGuid(), Title = "Machine Learning" }
                );
        }
    }
}
