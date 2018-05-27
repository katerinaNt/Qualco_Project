using System;
using System.Collections.Generic;
using System.Text;

namespace QualcoOne.Models
{
    public class TempImportedFile
    {
        //Constructor for TempImportedFile class
        public TempImportedFile()
        {

        }
        public string Vat { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompleteAddress { get; set; }
        public string County { get; set; }
        public string BillMunicipalityId { get; set; }
        public string BillDescription { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
    }
}