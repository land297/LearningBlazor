using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Shared.DbModels {
    public class Media {
        public int Id { get; set; }
        public string FullFileName { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
    }
}
