using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Learning.Shared.DbModels {
    public class CompletedSlideDeckProgram : IdEntity<CompletedSlideDeckProgram> {
        //public int Id { get; set; }
        [Required]
        public int UserAvatarId { get; set; }
  
        public UserAvatar UserAvatar { get; set; }
        [Required]
        public int SlideDeckProgramId { get; set; }
        public SlideDeckProgram SlideDeckProgram { get; set; }
        public string Comment { get; set; }
        [Range(0, 100, ErrorMessage = "Please choose a number between 0 and 100")]
        public int UserAvatarFeedback { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CompletedOn { get; set; }

        public override void CopyValues(CompletedSlideDeckProgram source, ref CompletedSlideDeckProgram copy) {
            throw new NotImplementedException();
        }
    }
}
