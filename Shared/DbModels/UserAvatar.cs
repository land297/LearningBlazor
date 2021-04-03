using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Learning.Shared.DbModels {
    // TODO remove interace from this file
    public interface ICoverImageable {
        public string CoverImage { get; }
        public Blob Blob { get; set; }
    }
    public class UserAvatar : IdEntity<UserAvatar>, ICoverImageable {
        //public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public string CoverImage { get { return Blob != null ? Blob.Data : string.Empty; } }
        
        public int? BlobId { get; set; }
        public Blob Blob { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        //TODO: is this a good..
        [JsonIgnore]
        public ICollection<CompletedSlideDeckProgram> CompletedSlideDeckPrograms { get; set; }

        [NotMapped]
        public IList<UserAccessSlideDeckProgram> PersonalProgramAccess { get; set; }
        public override void CopyValues(UserAvatar source, ref UserAvatar copy) {
            throw new NotImplementedException();
        }
    }
}
