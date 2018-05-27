using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualcoOne.API.Services
{
    public interface ISettlementService
    {
        decimal SettlementService(int SettlementTypeId, decimal BillSum, int NumberOfMounths);
    }
}
