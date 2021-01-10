using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Shared.DbModels {
    public abstract class IdEntity<T> {
        public int Id { get; set; }
        public abstract void CopyValues(T source, ref T copy);
    }
    public class UserAccessSlideDeckProgram : IdEntity<UserAccessSlideDeckProgram> {
        //public int Id { get; set; }
        public int SlideDeckProgramId { get; set; }
        public SlideDeckProgram SlideDeckProgram { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int? UserAvatarId { get; set; }
        public UserAvatar UserAvatar { get; set; }
        public string Comments { get; set; }

        public override void CopyValues(UserAccessSlideDeckProgram source, ref UserAccessSlideDeckProgram copy) {
            throw new NotImplementedException();
        }
    }
}
