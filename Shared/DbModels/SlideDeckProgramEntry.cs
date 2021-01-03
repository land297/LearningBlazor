using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Learning.Shared.DbModels {
    public class SlideDeckProgramEntry {
        public int Id { get; set; }
        public int SlideDeckProgramId { get; set; }
        [JsonIgnore]
        public SlideDeckProgram SlideDeckProgram { get; set; }
        public int SlideDeckId { get; set; }
        public SlideDeck SlideDeck { get; set; }
        public int Repititions { get; set; }
        public int Duration { get; set; }
        public string Comments { get; set; }
    }
}
