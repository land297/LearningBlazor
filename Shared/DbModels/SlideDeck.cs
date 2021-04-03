using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Learning.Shared.Models.Enums;

namespace Learning.Shared.DbModels {
    public class SlideDeck : IdEntity<SlideDeck>{
        
        public int AuthorId { get; set; }
        public User Author { get; set; }
        [RegularExpression("^[a-z0-9-]+$", ErrorMessage = "Slug format not valid.")]
        [StringLength(160)]
        public string Slug { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Categories { get; set; }
        public string CoverImage { get; set; }
        public int Views { get; set; }
        public double Rating { get; set; }
        public DateTime Published { get; set; }
        public bool IsPublished { get { return Published > DateTime.MinValue; } }
        public bool IsDeleted { get; set; }
        public bool Featured { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public IList<Slide> Slides { get; set; } = new List<Slide>();
        public ICollection<SlideDeckProgramEntry> Entries { get; set; }

        public override void CopyValues(SlideDeck source, ref SlideDeck copy) {
            throw new NotImplementedException();
        }
    }
    public class SlideDeckDTO {
        public int AuthorId { get; set; }
        public User Author { get; set; }
        [RegularExpression("^[a-z0-9-]+$", ErrorMessage = "Slug format not valid.")]
        [StringLength(160)]
        public string Slug { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Categories { get; set; }
        public string CoverImage { get; set; }
        public int Views { get; set; }
        public double Rating { get; set; }
        public DateTime Published { get; set; }
        public bool IsPublished { get { return Published > DateTime.MinValue; } }
        public bool Featured { get; set; }
        public IList<SlideDTO> Slides { get; set; } = new List<SlideDTO>();
    }
}
