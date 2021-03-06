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
    [Migration("20201228134229_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

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

                    b.Property<DateTime>("Published")
                        .HasColumnType("TEXT");

                    b.Property<double>("Rating")
                        .HasColumnType("REAL");

                    b.Property<string>("Slug")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<int>("Views")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("SlideDecks");
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

                    b.Property<int>("UserRoel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .HasMaxLength(16)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
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

            modelBuilder.Entity("Learning.Shared.DbModels.SlideDeck", b =>
                {
                    b.Navigation("Slides");
                });
#pragma warning restore 612, 618
        }
    }
}
