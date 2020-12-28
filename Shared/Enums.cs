using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Shared.Models {
    public class Enums {
        public enum SaveStatus {
            Saving = 1, Publishing = 2, Unpublishing = 3
        }

        public enum PublishedStatus {
            All, Published, Drafts, Featured
        }
        public enum AccessLevel {
            All, Premium, Free
        }
        public enum UserRole {
            Admin, ContentCreator, Basic
        }
    }
}
