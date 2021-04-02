using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Shared.DbModels {
    public class ForgottenPasswordRequest : IdEntity<ForgottenPasswordRequest> {
        public string Email { get; set; }
        public DateTime WhenAskedForNew { get; set; }
        public string Code { get; set; }

        public override void CopyValues(ForgottenPasswordRequest source, ref ForgottenPasswordRequest copy) {
            throw new NotImplementedException();
        }
    }
}
