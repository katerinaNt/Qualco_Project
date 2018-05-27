using QualcoOne.API.Data;
using QualcoOne.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace QualcoOne.API.Services
{
    public class SettlementService : ISettlementService
    {
        private readonly DataBase context_;

        public SettlementService(DataBase context)
        {
            context_ = context;
        }

        decimal ISettlementService.SettlementService(int SettlementTypeId, decimal BillSum, int NumberOfMounths)
        {
            var SettlementTypes = context_.SettlementTypes
                .Where(s => s.SettlementTypeId == SettlementTypeId);

            foreach (var SettlementType in SettlementTypes)
            {       
                decimal dp = BillSum * SettlementType.DownpaymentPercentage;
                decimal A1 = BillSum - dp;
                decimal R1 = SettlementType.Interest/12;
                decimal power = (decimal)Math.Pow((double)(1 + R1), (double)NumberOfMounths);
                decimal n = A1 * R1;
                decimal m = n * power;
                decimal Instalment = m / (power - 1);
                decimal Famount = Math.Round((Instalment * NumberOfMounths),2);
            }
<<<<<<< HEAD

            return 1;


=======
            return 1;
>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962
        }
    }
}
