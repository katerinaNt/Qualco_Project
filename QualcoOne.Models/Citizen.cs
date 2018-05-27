using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QualcoOne.Models
{
    public class Citizen
    {
        //Constructor forl Citizen class
        public Citizen()
        {
            Bills = new List<Bill>();
        }

        public string CitizenId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompleteAddress { get; set; }
        public string County { get; set; }
        
        public string Password { get; set; }
        public bool ChangePassword { get; set; }
        public bool SendEmail { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}