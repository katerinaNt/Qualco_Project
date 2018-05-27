using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QualcoOne.API.CitizenAccount;
using Microsoft.AspNetCore.Authorization;
using QualcoOne.API.Data;
using QualcoOne.API.ViewModels;
using QualcoOne.Models;

namespace QualcoOne.API.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly Services.IFileImportService fsvc_;
        private readonly Services.IEmailSenderCitizenService email_;
        private readonly Services.IFilesExportService exportService_;
        private readonly Services.ICitizenAccountService citizenService_;
        private readonly Services.ISettlementService settlementsvc_;

        private readonly DataBase context_;


<<<<<<< HEAD
        public HomeController(Services.IFileImportService fservice,
            Services.IEmailSenderCitizenService emailSender,
            Services.IFilesExportService exportService,
            Services.ICitizenAccountService citizenService,
            Services.ISettlementService settlementservice,
=======
        public HomeController(Services.IFileImportService fservice, 
            Services.IEmailSenderCitizenService emailSender,
            Services.IFilesExportService exportService,
            Services.ICitizenAccountService citizenService,
            Services.ISettlementService settlementservice, 
>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962
            DataBase context)
        {
            fsvc_ = fservice;
            email_ = emailSender;
            exportService_ = exportService;
            citizenService_ = citizenService;
            settlementsvc_ = settlementservice;
            context_ = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Debt()
        {
            ViewData["Message"] = "Your debt page.";

            return View();
        }


        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



<<<<<<< HEAD
=======

        

        
        


        public IActionResult Settlement()
        {
            decimal settlement = settlementsvc_.SettlementService(4, 500m, 36);
            return View();
        }











>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962
        //Call ImportFile Service
        public IActionResult ImportFile()
        {
            fsvc_.FileImportService();
<<<<<<< HEAD
            return RedirectToAction("Index", "Home");
        }



        //Call Sendemail Service
        public IActionResult Sendemail()
        {
            email_.EmailSenderCitizenService();
            return RedirectToAction("Index", "Home");
        }



        //Call ExportFiles Service
        public IActionResult ExportFiles()
        {
            exportService_.FilesExportService();
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        //Acount Details
        public async Task<IActionResult> AccountDetails(string LoginCitizenId = "111111111")
        {
            var AccountDetailsResult = await citizenService_.GetCitizenAccountAsync(LoginCitizenId);

            var viewModel = new AccountDetailsViewModel();

            if (!AccountDetailsResult.IsSuccess())
            {
                viewModel.CitizenId = "0";
            }
            else
            {
                viewModel.CitizenId = AccountDetailsResult.Data.CitizenId;
                viewModel.FirstName = AccountDetailsResult.Data.FirstName;
                viewModel.LastName = AccountDetailsResult.Data.LastName;
                viewModel.Email = AccountDetailsResult.Data.Email;
                viewModel.Phone = AccountDetailsResult.Data.Phone;
                viewModel.CompleteAddress = AccountDetailsResult.Data.CompleteAddress;
                viewModel.County = AccountDetailsResult.Data.County;
                viewModel.TypedCurrentPassword = "";
                viewModel.NewPassword = "";
                viewModel.VerifyNewPassword = "";
            }
            return View(viewModel);
        }



        [HttpPost]
        //UpdateCitizenPassword
        public IActionResult UpdateCitizenPassword(AccountDetailsViewModel inputmodel)
        {
            //var UpdateCitizenPasswordResult = await citizenService_.UpdateCitizenPassword(inputmodel.CitizenId,
            //    inputmodel.TypedCurrentPassword, inputmodel.NewPassword, inputmodel.VerifyNewPassword);

            var viewModel = new AccountDetailsViewModel();

            //Find typed password 
            var citizen = context_.Citizens
               .Where(c => c.CitizenId.Equals(inputmodel.CitizenId,
                   StringComparison.OrdinalIgnoreCase))
               .Where(c => c.Password == inputmodel.TypedCurrentPassword)
               .SingleOrDefault();

            if (citizen == null)
                inputmodel.PasswordResult = 1;
            else if (!(inputmodel.NewPassword == inputmodel.VerifyNewPassword))
                inputmodel.PasswordResult = 2;
            else
            {
                citizen.Password = inputmodel.NewPassword;
                context_.Citizens.Update(citizen);
                context_.SaveChanges();
                inputmodel.PasswordResult = 3;
            }
            return View("AccountDetails", inputmodel);
        }



        [HttpGet]
        //View Bills
        public IActionResult Bills(string LoginCitizenId = "111111111")
        {
            //Find Bills without Payment and Settlement
            var ViewBills = context_.Bills
               .Where(b => (b.PaymentId == null))
               .Where(b => (b.SettlementId == null))
               .Where(b => b.CitizenId == LoginCitizenId)
               .ToList();

            //Take Settlements Types
            var ViewSettlementsTypes = context_.SettlementTypes
                .ToList();



            var UnpaidBills = new BillsViewModel();

            UnpaidBills.Bills = ViewBills;
            UnpaidBills.SettlementTypes = ViewSettlementsTypes;

            return View(UnpaidBills);
        }



        [HttpGet]
        //View Payments
        public IActionResult Payments(string LoginCitizenId = "111111111")
        {
            //Find Bills without Payment and Settlement
            var ViewPayments = context_.Bills
               .Where(b => (!(b.PaymentId == null)))
               .Where(b => (b.SettlementId == null))
               .Where(b => b.CitizenId == LoginCitizenId)
               .ToList();

            return View(ViewPayments);
        }



        [HttpGet]
        //View Settlements
        public IActionResult Settlements(string LoginCitizenId = "111111111")
        {
            //Find Bills without Payment and Settlement
            var ViewSettlements = context_.Bills
               .Where(b => (b.PaymentId == null))
               .Where(b => (!(b.SettlementId == null)))
               .Where(b => b.CitizenId == LoginCitizenId)
               .ToList();

            return View(ViewSettlements);
        }



        [HttpPost]
        public IActionResult Paybill(int billId, string CreditcardNumber)
        {

            var Bill = context_.Bills
               .Where(b => (b.BillId == billId))
               .FirstOrDefault();

            var NewPayment = new Payment
            {
                Paymentamount = Bill.Amount,
                PaymentDateTime = DateTime.Now
            };

            Bill.Payment = NewPayment;
            context_.Bills.Update(Bill);

            context_.SaveChanges();


            return Ok();
        }




        [HttpPost]
        //CalculateSettlements
        public IActionResult CalculateSettlements(int[] billid, string SelectedInstallments, int SelectedSettlementTypeId)
        {
            decimal SelectedBillsSum = 0;
            var BillAmount = new Bill();
            int SelectedInstallmentsInt = Int32.Parse(SelectedInstallments);


            foreach (int BillId in billid)
            {

                BillAmount = context_.Bills
               .Where(b => (b.BillId == BillId))
               .FirstOrDefault();

                SelectedBillsSum = SelectedBillsSum + BillAmount.Amount;
            }



            var SettlementTypes = context_.SettlementTypes
                .Where(s => s.SettlementTypeId == SelectedSettlementTypeId)
                .SingleOrDefault();         
            
                decimal dp = SelectedBillsSum * SettlementTypes.DownpaymentPercentage;
                decimal A1 = SelectedBillsSum - dp;
                decimal R1 = SettlementTypes.Interest / 12;
                decimal power = (decimal)Math.Pow((double)(1 + R1), (double)SelectedInstallmentsInt);
                decimal n = A1 * R1;
                decimal m = n * power;
                decimal Instalment = (m / (power - 1));
                decimal Famount = Math.Round((Instalment * SelectedInstallmentsInt), 2);
                decimal ResultDP = Math.Round((dp), 2);
                decimal ResulInstalment = Math.Round((Instalment), 2);


            //decimal SettlementAmount = settlementsvc_.SettlementService(SelectedSettlementTypeId,
            //  SelectedBillsSum, SelectedInstallmentsInt);


            var poutses = new { SelectedBillsSum = SelectedBillsSum, ResultDP = ResultDP , ResulInstalment = ResulInstalment, Famount = Famount};

            //    foreach (Bill invoiceObj in inputmodel.Bills)
            //    {
            //        if () //billObj.IsChecked)
            //        {

            //        }
            //    }
            //var poutses = new { downpayment = 200, asdasd = 100 };
            //inputmodel.CitizenInstallments = 3;

            ////foreach (Bill invoiceObj in inputmodel.Bills)
            ////{
            ////    if (invoiceObj.IsChecked)
            ////    {
            ////        BillSum = invoiceObj.Amount;
            ////    }
            ////}
            return Ok(poutses);
            //decimal SettlementAmount = settlementsvc_.SettlementService(inputmodel.CitizenSettlementTypeId,
            //   BillSum, inputmodel.CitizenInstallments);

            //inputmodel.SettlementAmount = SettlementAmount;

            //    return RedirectToAction("Bills", inputmodel);

        }








        [HttpPost]
        //CalculateSettlements
        public IActionResult AddSettlements(int[] billid, string SelectedInstallments, int SelectedSettlementTypeId)
        {
            decimal SelectedBillsSum = 0;
            var BillAmount = new Bill();
            int SelectedInstallmentsInt = Int32.Parse(SelectedInstallments);


            foreach (int BillId in billid)
            {

                BillAmount = context_.Bills
               .Where(b => (b.BillId == BillId))
               .FirstOrDefault();

                SelectedBillsSum = SelectedBillsSum + BillAmount.Amount;
            }


            var SettlementTypes = context_.SettlementTypes
                .Where(s => s.SettlementTypeId == SelectedSettlementTypeId)
                .SingleOrDefault();

            decimal dp = SelectedBillsSum * SettlementTypes.DownpaymentPercentage;
            decimal A1 = SelectedBillsSum - dp;
            decimal R1 = SettlementTypes.Interest / 12;
            decimal power = (decimal)Math.Pow((double)(1 + R1), (double)SelectedInstallmentsInt);
            decimal n = A1 * R1;
            decimal m = n * power;
            decimal Instalment = (m / (power - 1));
            decimal Famount = Math.Round((Instalment * SelectedInstallmentsInt), 2);
            decimal ResultDP = Math.Round((dp), 2);
            decimal ResulInstalment = Math.Round((Instalment), 2);


            var NewSettlement = new Settlement
            {
                Downpayment = ResultDP,
                Installments = SelectedInstallmentsInt,
                MonthlyAmount = ResulInstalment,
                SettlementDateTime = DateTime.Now,
                SettlementTypeId = SelectedSettlementTypeId
            };

            //Bill.Payment = NewPayment;
            context_.Settlements.Add(NewSettlement);
            context_.SaveChanges();

            var Settlement = context_.Settlements
                .LastOrDefault();


            
            foreach (int BillId in billid)
            {
                var Bill = context_.Bills
                .Where(b => (b.BillId == BillId))
                .FirstOrDefault();

                Bill.SettlementId = Settlement.SettlementId;
                context_.Bills.Update(Bill);
                context_.SaveChanges();

            }
=======
            return View();
        }
>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962

        //Call Sendemail Service
        public IActionResult Sendemail()
        {
            email_.EmailSenderCitizenService();
            return View();
        }

        //Call ExportFiles Service
        public IActionResult ExportFiles()
        {
            exportService_.FilesExportService();
            return View();

        }


<<<<<<< HEAD


        //[HttpGet]
        ////CalculateSettlements
        //public IActionResult AddSettlement(int [] billid )
        //{


        //    //inputmodel.CitizenInstallments = 3;

        //    ////foreach (Bill invoiceObj in inputmodel.Bills)
        //    ////{
        //    ////    if (invoiceObj.IsChecked)
        //    ////    {
        //    ////        BillSum = invoiceObj.Amount;
        //    ////    }
        //    ////}

        //    //decimal SettlementAmount = settlementsvc_.SettlementService(inputmodel.CitizenSettlementTypeId,
        //    //   BillSum, inputmodel.CitizenInstallments);

        //    //inputmodel.SettlementAmount = SettlementAmount;
        //    return Ok();
        //    //return RedirectToAction("Bills", inputmodel);
        //}


        //public IActionResult Settlement()
        //{

        //    return View();
        //}






        //[HttpGet]
        ////AddPayment
        //public IActionResult AddPayment(string LoginCitizenId = "111111111")
        //{
        //    ////Find Bills without Payment and Settlement
        //    //var ViewBills = context_.Bills
        //    //   .Where(b => (b.PaymentId == null))
        //    //   .Where(b => (b.SettlementId == null))
        //    //   .Where(b => b.CitizenId == LoginCitizenId)
        //    //   .ToList();


        //    //var UnpaidBills = new BillsViewModel();

        //    //UnpaidBills.Bills = ViewBills;


        //    return View("Bills");
        //}



=======
        [HttpGet]
        //Acount Details
        public async Task<IActionResult> AccountDetails(string LoginCitizenId = "111111111")
        {
            var AccountDetailsResult = await citizenService_.GetCitizenAccountAsync(LoginCitizenId);

            var viewModel = new AccountDetailsViewModel();

            if (!AccountDetailsResult.IsSuccess())
            {
                viewModel.CitizenId = "0";
            }
            else
            {
                viewModel.CitizenId = AccountDetailsResult.Data.CitizenId;
                viewModel.FirstName = AccountDetailsResult.Data.FirstName;
                viewModel.LastName = AccountDetailsResult.Data.LastName;
                viewModel.Email = AccountDetailsResult.Data.Email;
                viewModel.Phone = AccountDetailsResult.Data.Phone;
                viewModel.CompleteAddress = AccountDetailsResult.Data.CompleteAddress;
                viewModel.County = AccountDetailsResult.Data.County;
                viewModel.Password = AccountDetailsResult.Data.Password;
            }
            return View(viewModel);
        }


        [HttpGet]
        //View Payments
        public IActionResult Payments(string LoginCitizenId = "111111111")
        {
            //Find Bills without Payment and Settlement
            var ViewPayments = context_.Bills
               .Where(b => (!(b.PaymentId == null)))
               .Where(b => (b.SettlementId == null))
               .Where(b => b.CitizenId == LoginCitizenId)
               .ToList();

            return View(ViewPayments);
        }


        [HttpGet]
        //View Settlements
        public IActionResult Settlements(string LoginCitizenId = "111111111")
        {
            //Find Bills without Payment and Settlement
            var ViewSettlements = context_.Bills
               .Where(b => (b.PaymentId == null))
               .Where(b => (!(b.SettlementId == null)))
               .Where(b => b.CitizenId == LoginCitizenId)
               .ToList();

            return View(ViewSettlements);
        }

        
        [HttpGet]
        //View Bills
        public IActionResult Bills(string LoginCitizenId = "111111111")
        {
            //Find Bills without Payment and Settlement
            var ViewBills = context_.Bills
               .Where(b => (b.PaymentId == null))
               .Where(b => (b.SettlementId == null))
               .Where(b => b.CitizenId == LoginCitizenId)
               .ToList();

            //Take Settlements Types
            var ViewSettlementsTypes = context_.SettlementTypes
                .ToList();



            var UnpaidBills = new BillsViewModel();

            UnpaidBills.Bills = ViewBills;
            UnpaidBills.SettlementTypes = ViewSettlementsTypes;

            return View(UnpaidBills);
        }

        




        [HttpGet]
        //AddPayment
        public IActionResult AddPayment(string LoginCitizenId = "111111111")
        {
            ////Find Bills without Payment and Settlement
            //var ViewBills = context_.Bills
            //   .Where(b => (b.PaymentId == null))
            //   .Where(b => (b.SettlementId == null))
            //   .Where(b => b.CitizenId == LoginCitizenId)
            //   .ToList();


            //var UnpaidBills = new BillsViewModel();

            //UnpaidBills.Bills = ViewBills;


            return View("Bills");
        }
>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962




<<<<<<< HEAD
=======


        [HttpPost]
        //UpdateCitizenPassword
        public async Task<IActionResult> UpdateCitizenPassword(AccountDetailsViewModel inputmodel)
        {
            var UpdateCitizenPasswordResult = await citizenService_.UpdateCitizenPassword(inputmodel.CitizenId,
                inputmodel.TypedCurrentPassword, inputmodel.NewPassword, inputmodel.VerifyNewPassword);

            var viewModel = new AccountDetailsViewModel();

            if (!UpdateCitizenPasswordResult.IsSuccess())
            {
                viewModel.Result = false;
            }
            else
            {
                viewModel.Password = "";
                viewModel.TypedCurrentPassword = "";
                viewModel.NewPassword = "";
                viewModel.VerifyNewPassword = "";
                viewModel.Result = true;
            }
            return View("AccountDetails", viewModel);
        }
>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962
    }
}
