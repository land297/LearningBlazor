using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Learning.Shared.Models.Enums;

namespace Learning.Shared.DbModels {
    public class SlideDeck {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
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
        public AccessLevel AccessLevel { get; set; }
        public IList<Slide> Slides { get; set; }
    }
}
