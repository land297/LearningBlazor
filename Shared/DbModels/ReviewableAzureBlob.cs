using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Shared.DbModels {
    public class ReviewableAzureBlob {
        public int Id { get; set; }
        public int AzureBlobId { get; set; }
        public AzureBlob AzureBlob { get; set; }
        public int CompletedProgramReviewableId { get; set; }
        public CompletedProgramReviewable CompletedProgramReviewable { get; set; }
    }
}
