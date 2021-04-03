﻿// <auto-generated />
using System;
using Learning.Server.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Learning.Server.Migrations.SqlServerMigrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Learning.Shared.DbModels.AzureBlob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompletedProgramReviewableId")
                        .HasColumnType("int");

                    b.Property<string>("Mime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uri")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompletedProgramReviewableId");

                    b.ToTable("AzureBlobs");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.Blob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Blobs");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.BlogPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessLevel")
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Categories")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoverImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Featured")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Published")
                        .HasColumnType("datetime2");

                    b.Property<string>("Slug")
                        .HasMaxLength(160)
                        .HasColumnType("nvarchar(160)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.CompletedProgramReviewable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompletedSlideDeckProgramId")
                        .HasColumnType("int");

                    b.Property<bool>("IsReviewReadByUser")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReviewed")
                        .HasColumnType("bit");

                    b.Property<string>("ReviewedComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserAvatarId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompletedSlideDeckProgramId");

                    b.HasIndex("UserAvatarId");

                    b.ToTable("CompletedProgramReviewables");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.CompletedSlideDeckProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CompletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<int?>("SlideDeckProgramId")
                        .HasColumnType("int");

                    b.Property<int>("UserAvatarFeedback")
                        .HasColumnType("int");

                    b.Property<int?>("UserAvatarId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SlideDeckProgramId");

                    b.HasIndex("UserAvatarId");

                    b.ToTable("CompletedSlideDeckPrograms");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.ForgottenPasswordRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("WhenAskedForNew")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ForgottenPasswordRequests");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.ReviewableAzureBlob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AzureBlobId")
                        .HasColumnType("int");

                    b.Property<int>("CompletedProgramReviewableId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AzureBlobId");

                    b.HasIndex("CompletedProgramReviewableId");

                    b.ToTable("ReviewableAzureBlobs");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.Slide", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Page")
                        .HasColumnType("int");

                    b.Property<int>("SlideDeckId")
                        .HasColumnType("int");

                    b.Property<string>("TextContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SlideDeckId");

                    b.ToTable("Slides");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.SlideDeck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessLevel")
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Categories")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoverImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Featured")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Published")
                        .HasColumnType("datetime2");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<string>("Slug")
                        .HasMaxLength(160)
                        .HasColumnType("nvarchar(160)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("SlideDecks");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.SlideDeckProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessLevel")
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Categories")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoverImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Featured")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Published")
                        .HasColumnType("datetime2");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<string>("Slug")
                        .HasMaxLength(160)
                        .HasColumnType("nvarchar(160)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("SlideDeckPrograms");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.SlideDeckProgramEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("Repititions")
                        .HasColumnType("int");

                    b.Property<int?>("SlideDeckId")
                        .HasColumnType("int");

                    b.Property<int?>("SlideDeckProgramId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SlideDeckId");

                    b.HasIndex("SlideDeckProgramId");

                    b.ToTable("SlideDeckProgramEntries");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.UserAccessSlideDeckProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SlideDeckProgramId")
                        .HasColumnType("int");

                    b.Property<int?>("UserAvatarId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SlideDeckProgramId");

                    b.HasIndex("UserAvatarId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAccessSlideDeckPrograms");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.UserAvatar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BlobId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BlobId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAvatars");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.AzureBlob", b =>
                {
                    b.HasOne("Learning.Shared.DbModels.CompletedProgramReviewable", null)
                        .WithMany("Content")
                        .HasForeignKey("CompletedProgramReviewableId");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.BlogPost", b =>
                {
                    b.HasOne("Learning.Shared.DbModels.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.CompletedProgramReviewable", b =>
                {
                    b.HasOne("Learning.Shared.DbModels.CompletedSlideDeckProgram", "CompletedSlideDeckProgram")
                        .WithMany()
                        .HasForeignKey("CompletedSlideDeckProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learning.Shared.DbModels.UserAvatar", "UserAvatar")
                        .WithMany()
                        .HasForeignKey("UserAvatarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CompletedSlideDeckProgram");

                    b.Navigation("UserAvatar");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.CompletedSlideDeckProgram", b =>
                {
                    b.HasOne("Learning.Shared.DbModels.SlideDeckProgram", "SlideDeckProgram")
                        .WithMany("Completeds")
                        .HasForeignKey("SlideDeckProgramId");

                    b.HasOne("Learning.Shared.DbModels.UserAvatar", "UserAvatar")
                        .WithMany("CompletedSlideDeckPrograms")
                        .HasForeignKey("UserAvatarId");

                    b.Navigation("SlideDeckProgram");

                    b.Navigation("UserAvatar");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.ReviewableAzureBlob", b =>
                {
                    b.HasOne("Learning.Shared.DbModels.AzureBlob", "AzureBlob")
                        .WithMany()
                        .HasForeignKey("AzureBlobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learning.Shared.DbModels.CompletedProgramReviewable", "CompletedProgramReviewable")
                        .WithMany()
                        .HasForeignKey("CompletedProgramReviewableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AzureBlob");

                    b.Navigation("CompletedProgramReviewable");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.Slide", b =>
                {
                    b.HasOne("Learning.Shared.DbModels.SlideDeck", "SlideDeck")
                        .WithMany("Slides")
                        .HasForeignKey("SlideDeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SlideDeck");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.SlideDeck", b =>
                {
                    b.HasOne("Learning.Shared.DbModels.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.SlideDeckProgram", b =>
                {
                    b.HasOne("Learning.Shared.DbModels.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.SlideDeckProgramEntry", b =>
                {
                    b.HasOne("Learning.Shared.DbModels.SlideDeck", "SlideDeck")
                        .WithMany("Entries")
                        .HasForeignKey("SlideDeckId");

                    b.HasOne("Learning.Shared.DbModels.SlideDeckProgram", "SlideDeckProgram")
                        .WithMany("Entries")
                        .HasForeignKey("SlideDeckProgramId");

                    b.Navigation("SlideDeck");

                    b.Navigation("SlideDeckProgram");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.UserAccessSlideDeckProgram", b =>
                {
                    b.HasOne("Learning.Shared.DbModels.SlideDeckProgram", "SlideDeckProgram")
                        .WithMany()
                        .HasForeignKey("SlideDeckProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learning.Shared.DbModels.UserAvatar", "UserAvatar")
                        .WithMany()
                        .HasForeignKey("UserAvatarId");

                    b.HasOne("Learning.Shared.DbModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("SlideDeckProgram");

                    b.Navigation("User");

                    b.Navigation("UserAvatar");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.UserAvatar", b =>
                {
                    b.HasOne("Learning.Shared.DbModels.Blob", "Blob")
                        .WithMany()
                        .HasForeignKey("BlobId");

                    b.HasOne("Learning.Shared.DbModels.User", "User")
                        .WithMany("UserAvatars")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blob");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.CompletedProgramReviewable", b =>
                {
                    b.Navigation("Content");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.SlideDeck", b =>
                {
                    b.Navigation("Entries");

                    b.Navigation("Slides");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.SlideDeckProgram", b =>
                {
                    b.Navigation("Completeds");

                    b.Navigation("Entries");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.User", b =>
                {
                    b.Navigation("UserAvatars");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.UserAvatar", b =>
                {
                    b.Navigation("CompletedSlideDeckPrograms");
                });
#pragma warning restore 612, 618
        }
    }
}
