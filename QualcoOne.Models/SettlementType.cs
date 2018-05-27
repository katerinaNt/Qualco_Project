using System;
using System.Collections.Generic;
using System.Text;

namespace QualcoOne.Models
{
    public class SettlementType
    {
        public SettlementType()
        {
            Settlements = new List<Settlement>();
        }
        public int SettlementTypeId { get; set; }
        public decimal DownpaymentPercentage { get; set; }
        public int NumOfInstallments { get; set; }
        public decimal Interest { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Settlement> Settlements { get; set; }
    }
}