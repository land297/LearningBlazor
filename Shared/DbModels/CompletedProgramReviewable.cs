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
        public bool IsReviewed { get; set; }

        public List<ReviewableAzureBlob> Content { get; set; }
        public override void CopyValues(CompletedProgramReviewable source, ref CompletedProgramReviewable copy) {
            throw new NotImplementedException();
        }
    }
}
