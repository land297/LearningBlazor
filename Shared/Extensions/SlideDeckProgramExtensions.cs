using Learning.Shared.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Shared.Extensions {
    public static class thisProgramProgramExtensions {
        public static void CopyValues(this SlideDeckProgram thisProgram, SlideDeckProgram otherProgram) {
            thisProgram.AuthorId = otherProgram.AuthorId;
            thisProgram.Author = otherProgram.Author;
            thisProgram.Slug = otherProgram.Slug;
            thisProgram.Title = otherProgram.Title;
            thisProgram.Description = otherProgram.Description;
            thisProgram.Categories = otherProgram.Categories;
            thisProgram.CoverImage = otherProgram.CoverImage;
            thisProgram.Views = otherProgram.Views;
            thisProgram.Rating = otherProgram.Rating;
            thisProgram.Published = otherProgram.Published;
            thisProgram.IsDeleted = otherProgram.IsDeleted;
            thisProgram.Featured = otherProgram.Featured;
            thisProgram.AccessLevel = otherProgram.AccessLevel;
            if (thisProgram.Entries?.Count == 0) {
                thisProgram.Entries = otherProgram.Entries;
            } else {
                var slidesToDelete = new List<SlideDeckProgramEntry>();
                // instead of x foreachloops and linq should be able to do in 1 pass if first transformed into dictionary or something
                foreach (var entry in thisProgram.Entries) {
                    var otherProgramSlide = otherProgram.Entries.FirstOrDefault(x => x.Id == entry.Id);
                    if (otherProgramSlide == null) {
                        slidesToDelete.Add(entry);
                    } else {
                        entry.Comments = otherProgramSlide.Comments;
                        entry.Repititions = otherProgramSlide.Repititions;
                        entry.Duration = otherProgramSlide.Duration;
                        entry.SlideDeck = otherProgramSlide.SlideDeck;
                        entry.SlideDeckId = otherProgramSlide.SlideDeckId;
                    }
                }
                foreach (var otherProgramSlide in otherProgram.Entries.Where(os => os.Id == default(int))) {
                    thisProgram.Entries.Add(otherProgramSlide);
                }
                foreach (var delete in slidesToDelete) {
                    // TODO : add IsDeleted property for soft-delete?
                    thisProgram.Entries.Remove(delete);
                }
            }
            foreach (var entry in thisProgram.Entries) {
                entry.SlideDeckProgram = thisProgram;
                entry.SlideDeckProgramId = thisProgram.Id;
            }
        }
    }
}
