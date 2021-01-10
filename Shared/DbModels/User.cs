using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Learning.Shared.Models.Enums;

namespace Learning.Shared.DbModels {
    public class User : IdEntity<User> {
        //public int Id { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [StringLength(16, ErrorMessage = "Your username is too long (16 characters max)")]
        public string Username { get; set; }
        public string Bio { get; set; }
        public bool IsConfirmed { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public UserRole UserRole { get; set; }
        public List<UserAvatar> UserAvatars { get; set; }

        public override void CopyValues(User source, ref User copy) {
            throw new NotImplementedException();
        }
    }
}
