﻿// <auto-generated />
using System;
using Htp.ITnews.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Htp.ITnews.Data.EntityFramework.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190426050455_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<bool>("IsActive")
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

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = new Guid("837d9f34-9b9c-4e90-bf6c-de8007cf2033"),
                            Title = "Java"
                        },
                        new
                        {
                            Id = new Guid("f1b82eb9-7236-4e05-865e-2172923418d6"),
                            Title = "C#"
                        },
                        new
                        {
                            Id = new Guid("c0509410-bc44-4b7d-9b21-18fce2eddb91"),
                            Title = "C++"
                        },
                        new
                        {
                            Id = new Guid("2c4f31b1-a111-4e74-ac42-68dc59ecb328"),
                            Title = "Algorithms"
                        },
                        new
                        {
                            Id = new Guid("9d06ed8b-fe39-4ad7-bd18-0e6ed6b65433"),
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

                    b.ToTable("Like");
                });

            modelBuilder.Entity("Htp.ITnews.Data.Contracts.Entities.News", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AuthorId");

                    b.Property<Guid>("CategoryId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("PostCount");

                    b.Property<float>("Rating")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0f);

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

                    b.ToTable("NewsTag");
                });

            modelBuilder.Entity("Htp.ITnews.Data.Contracts.Entities.Rating", b =>
                {
                    b.Property<Guid>("AppUserId");

                    b.Property<Guid>("NewsId");

                    b.Property<int>("Value");

                    b.HasKey("AppUserId", "NewsId");

                    b.HasIndex("NewsId");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("Htp.ITnews.Data.Contracts.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Tag");
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
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade);

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