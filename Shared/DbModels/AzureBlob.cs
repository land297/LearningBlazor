using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Shared.DbModels {
    public class AzureBlob : IdEntity<AzureBlob> {
        public string Uri { get; set; }
        public string Name { get; set; }
        public override void CopyValues(AzureBlob source, ref AzureBlob copy) {
            throw new NotImplementedException();
        }
    }
}
