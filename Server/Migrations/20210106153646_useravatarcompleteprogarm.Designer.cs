﻿// <auto-generated />
using System;
using Learning.Server.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Learning.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210106153646_useravatarcompleteprogarm")]
    partial class useravatarcompleteprogarm
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Learning.Shared.DbModels.CompletedSlideDeckProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CompletedOn")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SlideDeckProgramId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserAvatarFeedback")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserAvatarId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SlideDeckProgramId");

                    b.HasIndex("UserAvatarId");

                    b.ToTable("CompletedSlideDeckPrograms");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullFileName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.Slide", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<int>("Page")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SlideDeckId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TextContent")
                        .HasColumnType("TEXT");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SlideDeckId");

                    b.ToTable("Slides");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.SlideDeck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Categories")
                        .HasColumnType("TEXT");

                    b.Property<string>("CoverImage")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Featured")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Published")
                        .HasColumnType("TEXT");

                    b.Property<double>("Rating")
                        .HasColumnType("REAL");

                    b.Property<string>("Slug")
                        .HasMaxLength(160)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Views")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("SlideDecks");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.SlideDeckProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Categories")
                        .HasColumnType("TEXT");

                    b.Property<string>("CoverImage")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Featured")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Published")
                        .HasColumnType("TEXT");

                    b.Property<double>("Rating")
                        .HasColumnType("REAL");

                    b.Property<string>("Slug")
                        .HasMaxLength(160)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserAvatarId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Views")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("UserAvatarId");

                    b.ToTable("SlideDeckPrograms");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.SlideDeckProgramEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comments")
                        .HasColumnType("TEXT");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Repititions")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SlideDeckId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SlideDeckProgramId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SlideDeckId");

                    b.HasIndex("SlideDeckProgramId");

                    b.ToTable("SlideDeckProgramEntries");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bio")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("BLOB");

                    b.Property<int>("UserRole")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .HasMaxLength(16)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.UserAccessSlideDeckProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comments")
                        .HasColumnType("TEXT");

                    b.Property<int>("SlideDeckProgramId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserAvatarId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

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
                        .HasColumnType("INTEGER");

                    b.Property<string>("CoverImage")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserAvatars");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.CompletedSlideDeckProgram", b =>
                {
                    b.HasOne("Learning.Shared.DbModels.SlideDeckProgram", "SlideDeckProgram")
                        .WithMany()
                        .HasForeignKey("SlideDeckProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learning.Shared.DbModels.UserAvatar", "UserAvatar")
                        .WithMany()
                        .HasForeignKey("UserAvatarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SlideDeckProgram");

                    b.Navigation("UserAvatar");
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

                    b.HasOne("Learning.Shared.DbModels.UserAvatar", null)
                        .WithMany("CompletedSlideDeckPrograms")
                        .HasForeignKey("UserAvatarId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.SlideDeckProgramEntry", b =>
                {
                    b.HasOne("Learning.Shared.DbModels.SlideDeck", "SlideDeck")
                        .WithMany()
                        .HasForeignKey("SlideDeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Learning.Shared.DbModels.SlideDeckProgram", "SlideDeckProgram")
                        .WithMany("Entries")
                        .HasForeignKey("SlideDeckProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

                    b.HasOne("Learning.Shared.DbModels.User", "UserAvatar")
                        .WithMany()
                        .HasForeignKey("UserAvatarId");

                    b.HasOne("Learning.Shared.DbModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SlideDeckProgram");

                    b.Navigation("User");

                    b.Navigation("UserAvatar");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.UserAvatar", b =>
                {
                    b.HasOne("Learning.Shared.DbModels.User", "User")
                        .WithMany("UserAvatars")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.SlideDeck", b =>
                {
                    b.Navigation("Slides");
                });

            modelBuilder.Entity("Learning.Shared.DbModels.SlideDeckProgram", b =>
                {
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
