using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Learning.Shared.DbModels {
    // TODO remove interace from this file
    public interface ICoverImageable {
        public string CoverImage { get; set; }
    }
    public class UserAvatar : ICoverImageable {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        //TODO: is this a good..
        [JsonIgnore]
        public List<CompletedSlideDeckProgram> CompletedSlideDeckPrograms { get; set; }
    }
}
