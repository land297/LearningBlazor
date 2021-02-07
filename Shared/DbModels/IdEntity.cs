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
}
