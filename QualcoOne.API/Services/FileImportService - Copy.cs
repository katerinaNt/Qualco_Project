using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QualcoOne.API.CitizenAccount;
using QualcoOne.API.Data;
using QualcoOne.Models;
using System;
using System.Globalization;
using System.Linq;

namespace QualcoOne.API.Services
{
    public class FileImportService : IFileImportService
    {
        private readonly DataBase context_;
        private readonly ApplicationDbContext _userManager;

        public FileImportService(DataBase context, ApplicationDbContext userManager)
        {
            context_ = context;
            _userManager = userManager;

        }

        void IFileImportService.FileImportService()
        {
            //Import from txt file to TempImportedFile
            string ImportPath = @"..\ToImport\";
            string ImportFilename = "Debts_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            var NewData = System.IO.File.ReadAllLines(ImportPath + ImportFilename).Skip(1);

            foreach (string line in NewData)
            {
                var cols = line.Split(";");
                var newTempRow = new TempImportedFile
                {
                    Vat = cols[0],
                    FirstName = cols[1],
                    LastName = cols[2],
                    Email = cols[3],
                    Phone = cols[4],
                    CompleteAddress = cols[5],
                    County = cols[6],
                    BillMunicipalityId = cols[7],
                    BillDescription = cols[8],
                    Amount = Convert.ToDecimal(cols[9]),
                    DueDate = DateTime.ParseExact(cols[10], "yyyyMMdd", CultureInfo.InvariantCulture)
                };
                context_.TempImportedFiles.Add(newTempRow);
            }
            context_.SaveChanges();

            //Transfer rows from TempImportedFile to Citizen + Bill
            var TempImportedFiles = context_.TempImportedFiles;
            var Citizens = context_.Citizens;

            foreach (var TempCitizen in TempImportedFiles)
            {
                var Citizens_query = context_.Citizens
                .Where(c => c.CitizenId == TempCitizen.Vat);

                //if the citizen is new then import to citizen + bill
                if (Citizens_query.Count() == 0)
                {
                    var NewCitizen = new Citizen
                    {
                        CitizenId = TempCitizen.Vat,
                        FirstName = TempCitizen.FirstName,
                        LastName = TempCitizen.LastName,
                        Email = TempCitizen.Email,
                        Phone = TempCitizen.Phone,
                        CompleteAddress = TempCitizen.CompleteAddress,
                        County = TempCitizen.County,
                        Password = "12345",
                        ChangePassword = true,
                        SendEmail = true
                    };
                    var NewCitizenAuth = new AppUser
                    {
                        UserName = TempCitizen.Vat,
                        AccessFailedCount = 0,
                        EmailConfirmed = true,
                        LockoutEnabled = true,
                        PhoneNumberConfirmed = true,
                        TwoFactorEnabled = false,
                        PasswordHash = "12345"
                    };


                    
                    //var user = new ApplicationUser { UserName = TempCitizen.Vat, Email = TempCitizen.Vat };
                    //var result = _userManager.CreateAsync(user, "123!@#aZ");
                    
                    var NewBill = new Bill
                    {
                        Amount = TempCitizen.Amount,
                        BillDescription = TempCitizen.BillDescription,
                        BillMunicipalityId = TempCitizen.BillMunicipalityId,
                        CitizenId = TempCitizen.Vat,
                        DueDate = TempCitizen.DueDate
                    };
                   // context_.AspNetUsers.Add(NewCitizenAuth);
                    context_.Citizens.Add(NewCitizen);
                    context_.Bills.Add(NewBill);
                }
                //else if we have the citizen then import only to bill
                else
                {
                    var NewBill = new Bill
                    {
                        Amount = TempCitizen.Amount,
                        BillDescription = TempCitizen.BillDescription,
                        BillMunicipalityId = TempCitizen.BillMunicipalityId,
                        CitizenId = TempCitizen.Vat,
                        DueDate = TempCitizen.DueDate
                    };
                    context_.Bills.Add(NewBill);
                }
            }
            context_.SaveChanges();
        }
    }
}
