using Learning.Shared.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Shared.Extensions {
    public static class SlideDeckExtensions {
        public static void CopyValues(this SlideDeck slidedeck, SlideDeck other) {
            slidedeck.AuthorId = other.AuthorId;
            slidedeck.Author = other.Author;
            slidedeck.Slug = other.Slug;
            slidedeck.Title = other.Title;
            slidedeck.Description = other.Description;
            slidedeck.Categories = other.Categories;
            slidedeck.CoverImage = other.CoverImage;
            slidedeck.Views = other.Views;
            slidedeck.Rating = other.Rating;
            slidedeck.Published = other.Published;
            slidedeck.IsDeleted = other.IsDeleted;
            slidedeck.Featured = other.Featured;
            slidedeck.AccessLevel = other.AccessLevel;
            if (slidedeck.Slides?.Count == 0) {
                slidedeck.Slides = other.Slides;
            } else {
                var slidesToDelete = new List<Slide>();
                // instead of x foreachloops and linq should be able to do in 1 pass if first transformed into dictionary or something
                foreach (var slide in slidedeck.Slides) {
                    var otherSlide = other.Slides.FirstOrDefault(x => x.Id == slide.Id);
                    if (otherSlide == null) {
                        slidesToDelete.Add(slide);
                    } else {
                        slide.Page = otherSlide.Page;
                        slide.TextContent = otherSlide.TextContent;
                        slide.ImageUrl = otherSlide.ImageUrl;
                        slide.VideoUrl = otherSlide.VideoUrl;
                    }
                }
                foreach (var otherSlide in other.Slides.Where(os => os.Id == default(int))) {
                    slidedeck.Slides.Add(otherSlide);
                }
                foreach (var delete in slidesToDelete) {
                    // TODO : add IsDeleted property for soft-delete?
                    slidedeck.Slides.Remove(delete);
                }
            }
            foreach (var slide in slidedeck.Slides) {
                slide.SlideDeck = slidedeck;
                slide.SlideDeckId = slidedeck.Id;
            }
        }
    }
}
