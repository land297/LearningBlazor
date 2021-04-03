using Learning.Shared.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.DbContext {
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
            this.Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // works because of naming convetion
            //modelBuilder.Entity<OwnerExerciseAccess>().HasKey(cs => new { cs.ExerciseId, cs.OwnerId });
            //modelBuilder.Entity<OwnerExerciseProgramAccess>().HasKey(cs => new { cs.ExerciseProgramId, cs.OwnerId });
            //modelBuilder.Entity<SlideDeckProgramEntry>().WillCascadeOnDelete(false);
            //modelBuilder.Entity<SlideDeckProgramEntry>()

            //.WithMany()
            //.WillCascadeOnDelete(false);
            //modelBuilder.Entity<SlideDeckProgramEntry>()
            //.HasOne(r => r.SlideDeck)
            //.WithMany()
            //.OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<SlideDeckProgramEntry>()
            //    .HasOne(r => r.SlideDeckProgram)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.Restrict);


            //modelBuilder.Entity<SlideDeckProgramEntry>()
            //    .HasOne<SlideDeck>(e => e.SlideDeck)
            //    .WithMany(d => d.Entries)
            //    .HasForeignKey(e => e.SlideDeckId)
            //    .IsRequired(true)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<SlideDeckProgramEntry>()
            //    .HasOne<SlideDeckProgram>(e => e.SlideDeckProgram)
            //    .WithMany(d => d.Entries)
            //    .HasForeignKey(e => e.SlideDeckProgramId)
            //    .IsRequired(true)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<CompletedSlideDeckProgram>()
            //  .HasOne<UserAvatar>(e => e.UserAvatar)
            //  .WithMany(d => d.CompletedSlideDeckPrograms)
            //  .HasForeignKey(e => e.UserAvatarId)
            //  .IsRequired(true)
            //  .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<CompletedSlideDeckProgram>()
            //  .HasOne<SlideDeckProgram>(e => e.SlideDeckProgram)
            //  .WithMany(d => d.Completeds)
            //  .HasForeignKey(e => e.SlideDeckProgramId)
            //  .IsRequired(true)
            //  .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
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
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<ForgottenPasswordRequest> ForgottenPasswordRequests { get; set; }

    }
}
