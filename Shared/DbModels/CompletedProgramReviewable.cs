using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Shared.DbModels {
    public class CompletedProgramReviewable : IdEntity<CompletedProgramReviewable> {

        [Required]
        public int CompletedSlideDeckProgramId { get; set; }
        public CompletedSlideDeckProgram CompletedSlideDeckProgram { get; set; }
        [Required]
        public int UserAvatarId { get; set; }
        public UserAvatar UserAvatar { get; set; }
        public bool IsReviewed { get; set; }
        public bool IsReviewReadByUser { get; set; }
        public string ReviewedComment { get; set; }

        public List<AzureBlob> Content { get; set; } = new List<AzureBlob>();
        public override void CopyValues(CompletedProgramReviewable source, ref CompletedProgramReviewable copy) {
            throw new NotImplementedException();
        }
    }
}
