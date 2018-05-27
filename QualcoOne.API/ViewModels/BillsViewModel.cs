using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualcoOne.API.ViewModels
{
    public class BillsViewModel
    {
        
        

<<<<<<< HEAD
        public int CitizenSettlementTypeId { get; set; }
        public int CitizenInstallments { get; set; }
        public decimal SettlementAmount { get; set; }
=======
        public int SettlementTypeId { get; set; }
        
>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962

        public IList<QualcoOne.Models.Bill> Bills { get; set; }
        public IList<QualcoOne.Models.SettlementType> SettlementTypes { get; set; }
    }
}
