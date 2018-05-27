using System;
using System.Collections.Generic;
using System.Text;

namespace QualcoOne.Models
{
    public class Settlement
    {
        public Settlement()
        {
            Bills = new List<Bill>();
        }

        public int SettlementId { get; set; }
        public decimal Downpayment { get; set; }
        public int Installments { get; set; }
        public decimal MonthlyAmount { get; set; }
        public DateTime SettlementDateTime { get; set; }

        public int SettlementTypeId { get; set; }
        public virtual SettlementType SettlementType { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}