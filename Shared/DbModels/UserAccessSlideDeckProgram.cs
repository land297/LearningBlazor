using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Shared.DbModels {
    public class UserAccessSlideDeckProgram {
        public int Id { get; set; }
        public int SlideDeckProgramId { get; set; }
        public SlideDeckProgram SlideDeckProgram { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int? UserAvatarId { get; set; }
        public User UserAvatar { get; set; }
        public string Comments { get; set; }
    }
}
