﻿using Learning.Shared.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.DbContext {
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<SlideDeck> SlideDecks { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<SlideDeckProgram> SlideDeckPrograms { get; set; }
        public DbSet<SlideDeckProgramEntry> SlideDeckProgramEntries { get; set; }
        public DbSet<UserAccessSlideDeckProgram> UserAccessSlideDeckPrograms { get; set; }
        public DbSet<CompletedSlideDeckProgram> CompletedSlideDeckPrograms { get; set; }
        public DbSet<UserAvatar> UserAvatars { get; set; }
        public DbSet<Blob> Blobs { get; set; }
        public DbSet<AzureBlob> AzureBlobs { get; set; }
        public DbSet<CompletedProgramReviewable> CompletedProgramReviewables { get; set; }
        public DbSet<ReviewableAzureBlob> ReviewableAzureBlobs { get; set; }

    }
}
