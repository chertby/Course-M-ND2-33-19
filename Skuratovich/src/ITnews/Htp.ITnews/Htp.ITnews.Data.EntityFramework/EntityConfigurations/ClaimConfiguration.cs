﻿using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Htp.ITnews.Data.EntityFramework.EntityConfigurations
{
    //public class ClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
    //{
    //    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
    //    {
    //        builder.HasData(
    //            new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
    //            new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Writer", NormalizedName = "WRITER" },
    //            new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Reader", NormalizedName = "READER" }
    //            );
    //    }

    //    public void Configure(EntityTypeBuilder<IdentityUserClaim<Guid>> builder)
    //    {
    //        builder.HasData(
    //            new IdentityUserClaim<Guid> { Id = Guid.NewGuid(), ClaimType);
    //    }
    //}
}
