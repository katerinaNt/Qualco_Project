using System;
using System.Collections.Generic;
using System.Text;

namespace QualcoOne.Models
{
    public class Bill
    {
        //Constructor forl Bill class
        public Bill()
        {

        }

        public int BillId { get; set; }
        public string BillDescription { get; set; }
        public string BillMunicipalityId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsChecked { get; set; }

        public string CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }

        public int? PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

        public int? SettlementId { get; set; }
        public virtual Settlement Settlement { get; set; }
    }
}