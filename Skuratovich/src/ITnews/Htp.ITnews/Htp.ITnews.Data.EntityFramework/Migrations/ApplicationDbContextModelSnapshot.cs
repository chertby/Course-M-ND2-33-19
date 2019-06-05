﻿// <auto-generated />
using System;
using Htp.ITnews.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Htp.ITnews.Data.EntityFramework.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("Htp.ITnews.Data.Contracts.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Htp.ITnews.Data.Contracts.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Сategories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d931b938-261d-484b-8418-aa14e63583b7"),
                            Title = "Java"
                        },
                        new
                        {
                            Id = new Guid("5b60e7ec-14b3-43c0-8d9b-7a653e332f1f"),
                            Title = "C#"
                        },
                        new
                        {
                            Id = new Guid("7c205f37-99bd-44d3-898c-57d29ad0a9ba"),
                            Title = "C++"
                        },
                        new
                        {
                            Id = new Guid("35536e5b-dfde-468e-bcd2-40851a93a552"),
                            Title = "Algorithms"
                        },
                        new
                        {
                            Id = new Guid("7e9af847-bd0b-4e40-a838-1c3b2e2b65c6"),
                            Title = "Machine Learning"
                        });
                });

            modelBuilder.Entity("Htp.ITnews.Data.Contracts.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AuthorId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("LikesCount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<Guid?>("NewsId");

                    b.Property<DateTime>("Updated");

                    b.Property<Guid?>("UpdatedById");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("NewsId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Htp.ITnews.Data.Contracts.Entities.Like", b =>
                {
                    b.Property<Guid>("AppUserId");

                    b.Property<Guid>("CommentId");

                    b.HasKey("AppUserId", "CommentId");

                    b.HasIndex("CommentId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Htp.ITnews.Data.Contracts.Entities.News", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AuthorId");

                    b.Property<Guid>("CategoryId");

                    b.Property<int>("CommentCount");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<decimal>("Rating");

                    b.Property<int>("RatingCount");

                    b.Property<int>("RatingSum");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<DateTime>("Updated");

                    b.Property<Guid?>("UpdatedById");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Htp.ITnews.Data.Contracts.Entities.NewsTag", b =>
                {
                    b.Property<Guid>("NewsId");

                    b.Property<Guid>("TagId");

                    b.HasKey("NewsId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("NewsTags");
                });

            modelBuilder.Entity("Htp.ITnews.Data.Contracts.Entities.Rating", b =>
                {
                    b.Property<Guid>("AppUserId");

                    b.Property<Guid>("NewsId");

                    b.Property<int>("Value");

                    b.HasKey("AppUserId", "NewsId");

                    b.HasIndex("NewsId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Htp.ITnews.Data.Contracts.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fb6449d0-1b21-4547-addc-eb3082de6b95"),
                            Title = "C"
                        },
                        new
                        {
                            Id = new Guid("3fbc3c4c-405a-4511-a613-a08aa4f11d14"),
                            Title = "C++"
                        },
                        new
                        {
                            Id = new Guid("75723417-6e91-42c8-858f-4c76bd6294de"),
                            Title = "C#"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("81911ac6-b266-4776-83fc-ab57b9434df8"),
                            ConcurrencyStamp = "fdbd1ebc-640a-4965-a041-bf2970bf5f4c",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = new Guid("62fb1d8e-0a3d-471a-bcb8-a06deff1c352"),
                            ConcurrencyStamp = "407aaf9a-ac72-4153-b8aa-af8c001c03cb",
                            Name = "Writer",
                            NormalizedName = "WRITER"
                        },
                        new
                        {
                            Id = new Guid("e6474039-ec84-4a03-a31b-f93821733281"),
                            ConcurrencyStamp = "a55306a4-952e-41a9-9dea-90f447d3a73b",
                            Name = "Reader",
                            NormalizedName = "READER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Htp.ITnews.Data.Contracts.Entities.Comment", b =>
                {
                    b.HasOne("Htp.ITnews.Data.Contracts.Entities.AppUser", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId");

                    b.HasOne("Htp.ITnews.Data.Contracts.Entities.News", "News")
                        .WithMany("Comments")
                        .HasForeignKey("NewsId");

                    b.HasOne("Htp.ITnews.Data.Contracts.Entities.AppUser", "UpdatedBy")
                        .WithMany("UpdatedComments")
                        .HasForeignKey("UpdatedById");
                });

            modelBuilder.Entity("Htp.ITnews.Data.Contracts.Entities.Like", b =>
                {
                    b.HasOne("Htp.ITnews.Data.Contracts.Entities.AppUser", "AppUser")
                        .WithMany("Likes")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Htp.ITnews.Data.Contracts.Entities.Comment", "Comment")
                        .WithMany("Likes")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Htp.ITnews.Data.Contracts.Entities.News", b =>
                {
                    b.HasOne("Htp.ITnews.Data.Contracts.Entities.AppUser", "Author")
                        .WithMany("News")
                        .HasForeignKey("AuthorId");

                    b.HasOne("Htp.ITnews.Data.Contracts.Entities.Category", "Category")
                        .WithMany("News")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Htp.ITnews.Data.Contracts.Entities.AppUser", "UpdatedBy")
                        .WithMany("UpdatedNews")
                        .HasForeignKey("UpdatedById");
                });

            modelBuilder.Entity("Htp.ITnews.Data.Contracts.Entities.NewsTag", b =>
                {
                    b.HasOne("Htp.ITnews.Data.Contracts.Entities.News", "News")
                        .WithMany("NewsTags")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Htp.ITnews.Data.Contracts.Entities.Tag", "Tag")
                        .WithMany("NewsTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Htp.ITnews.Data.Contracts.Entities.Rating", b =>
                {
                    b.HasOne("Htp.ITnews.Data.Contracts.Entities.AppUser", "AppUser")
                        .WithMany("Ratings")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Htp.ITnews.Data.Contracts.Entities.News", "News")
                        .WithMany("Ratings")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Htp.ITnews.Data.Contracts.Entities.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Htp.ITnews.Data.Contracts.Entities.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Htp.ITnews.Data.Contracts.Entities.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Htp.ITnews.Data.Contracts.Entities.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
