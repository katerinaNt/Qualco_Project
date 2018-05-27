using QualcoOne.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QualcoOne.API.ViewModels
{
    public class AccountDetailsViewModel
    {
        public string CitizenId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompleteAddress { get; set; }
        public string County { get; set; }

        public string Password { get; set; }
        public bool ChangePassword { get; set; }

        [MaxLength(20)]
        public string TypedCurrentPassword { get; set; }

        [MaxLength(20)]
        public string NewPassword { get; set; }

        [MaxLength(20)]
        public string VerifyNewPassword { get; set; }

<<<<<<< HEAD
        public byte PasswordResult { get; set; }
=======
>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962
        public bool Result { get; set; }
    }
}
