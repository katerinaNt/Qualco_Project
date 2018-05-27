using Microsoft.EntityFrameworkCore;
using QualcoOne.Models;
using System.Linq;
using System;
using System.Globalization;
using QualcoOne.API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace QualcoOne.API.Services
{
    public class FilesExportService : IFilesExportService
    {
        private readonly DataBase context_;

        public FilesExportService(DataBase context)
        {
            context_ = context;
        }

        void IFilesExportService.FilesExportService()
        {
<<<<<<< HEAD
            var Delimiter = ";";
            var Newline = "\r\n";
            string ExportPath = @"..\ToExport\";
=======
            var Delimiter = ",";
            var Newline = "\r\n";
            string ExportPath = @"..\toexport\";
>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962

            string PaymentsFileName = "PAYMENTS_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string SettlementsFileName = "SETTLEMENTS_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            var PaymentsText = new StringBuilder("BILL_ID;TIME;AMOUNT;METHOD\r\n");
            var Pquery = from p in context_.Payments
                         join b in context_.Bills on p.PaymentId equals b.PaymentId
                         select new
                         {
                             PqBillId = b.BillMunicipalityId,
                             PqPayDate = p.PaymentDateTime,
                             PqPayAmount = p.Paymentamount
                         };

            foreach (var pay in Pquery)
            {
                PaymentsText
                    .Append(pay.PqBillId).Append(Delimiter)
<<<<<<< HEAD
                    .Append(pay.PqPayDate.ToString("yyyy-MM-ddThh:mm:ssZ")).Append(Delimiter)
=======
                    .Append(pay.PqPayDate).Append(Delimiter)
>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962
                    .Append(pay.PqPayAmount).Append(Delimiter)
                    .Append("CREDIT").Append(Newline);
            }

            var SettlementsText = new StringBuilder("VAT;TIME;BILLS;DOWNPAYMENT;INSTALLMENTS;INTEREST\r\n");

            var TheSettlements = from settlement in context_.Settlements
                                 join st in context_.SettlementTypes on settlement.SettlementTypeId equals st.SettlementTypeId
                                 select new
                                 {
                                     SettlementCitizId = settlement.Bills.First(b => b.SettlementId == settlement.SettlementId).CitizenId,
                                     SettlementTime = settlement.SettlementDateTime,
                                     SettlementBills = (from b in settlement.Bills select b.BillMunicipalityId).ToList(),
                                     SettlementDownP = settlement.Downpayment,
                                     SettlementInstall = settlement.Installments,
                                     SettlementInter = st.Interest
                                 };

            // Get the results in memory:
            var results = TheSettlements.ToArray();

            // Format the results:
            var printResults = results.Select(s =>
                  s.SettlementCitizId.ToString() + ";" +
                  s.SettlementTime.ToString("yyyy-MM-ddThh:mm:ssZ") + ";" +
                  string.Join(",", s.SettlementBills) + ";" +
                  s.SettlementDownP.ToString() + ";" +
                  s.SettlementInstall.ToString() + ";" +
                  s.SettlementInter.ToString() + Newline
                  );

            foreach (var result in printResults)
            {
                SettlementsText.Append(result);
            }

            System.IO.File.WriteAllText(ExportPath + PaymentsFileName, PaymentsText.ToString());
            System.IO.File.WriteAllText(ExportPath + SettlementsFileName, SettlementsText.ToString());

            //Clear Settlements table
            context_.Bills.RemoveRange(from b in context_.Bills select b);
            context_.Payments.RemoveRange(from p in context_.Payments select p);
            context_.Settlements.RemoveRange(from s in context_.Settlements select s);
            context_.SaveChanges();
        }
    }
}