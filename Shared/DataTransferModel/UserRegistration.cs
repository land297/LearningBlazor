using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Shared.DataTransferModel {
    public class UserRegistration {
        [Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Bio { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The passwords do not match")]
        public string Password2 { get; set; }
    }
}
