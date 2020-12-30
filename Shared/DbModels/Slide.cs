using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Learning.Shared.DbModels {
    public class Slide {
        public int Id { get; set; }
        public int SlideDeckId { get; set; }
        [JsonIgnore]
        public SlideDeck SlideDeck { get; set; }
        public int Page { get; set; }
        public string TextContent { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
    }
    public class SlideDTO {
        public int Page { get; set; }
        public string TextContent { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
    }
}
