<<<<<<< HEAD
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using QualcoOne.API.Models;
using QualcoOne.API.Data;
//using QualcoOne.API.Models;
=======
﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QualcoOne.API.CitizenAccount;
using QualcoOne.API.Data;
>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962
using QualcoOne.Models;
using System;
using System.Globalization;
using System.Linq;
<<<<<<< HEAD
using System.Net;
using System.Net.Mail;
=======
>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962

namespace QualcoOne.API.Services
{
    public class FileImportService : IFileImportService
    {
        private readonly DataBase context_;
        private readonly ApplicationDbContext _userManager;
<<<<<<< HEAD
        //private readonly ApplicationRole _userRole;
=======
>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962

        public FileImportService(DataBase context, ApplicationDbContext userManager)
        {
            context_ = context;
            _userManager = userManager;
<<<<<<< HEAD
            //_userRole = userRole;
=======

>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962
        }
        
        void IFileImportService.FileImportService()
        {
<<<<<<< HEAD
            try
            {
                // clear TempImportedFile table
                var Tempdata = (from n in context_.TempImportedFiles select n);
                context_.TempImportedFiles.RemoveRange(Tempdata);
                context_.SaveChanges();

                // import from txt file to TempImportedFile
                string ImportPath = @"..\ToImport\";
                string ImportFilename = "Debts_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
=======
            //Import from txt file to TempImportedFile
            string ImportPath = @"..\ToImport\";
            string ImportFilename = "Debts_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962

                var NewData = System.IO.File.ReadAllLines(ImportPath + ImportFilename).Skip(1);

                foreach (string line in NewData)
                {
<<<<<<< HEAD
                    var cols = line.Split(";");

                    // skip rows with existing billids
                    string CurrentBillId = cols[7];
                    bool Exists = false;
                    Exists = context_.TempImportedFiles.Any(c => c.BillMunicipalityId == CurrentBillId);
                    if (Exists == true) { continue; }
=======
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
>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962

                    // create a new temp row from file
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
<<<<<<< HEAD
                    context_.TempImportedFiles.Add(newTempRow);
=======
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
>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962
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
<<<<<<< HEAD
                        var NewCitizen = new Citizen
                        {
                            CitizenId = TempCitizen.Vat,
                            FirstName = TempCitizen.FirstName,
                            LastName = TempCitizen.LastName,
                            Email = TempCitizen.Email,
                            Phone = TempCitizen.Phone,
                            CompleteAddress = TempCitizen.CompleteAddress,
                            County = TempCitizen.County,
                            Password = PasswordGenerator.Generate(8, 2),
                            ChangePassword = true,
                            SendEmail = false
                        };

                        // send credentials to citizen
                        SmtpClient ss = new SmtpClient("smtp.gmail.com", 587);
                        ss.EnableSsl = true;
                        ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                        ss.UseDefaultCredentials = false;
                        ss.Credentials = new NetworkCredential("qualco.one@gmail.com", "QUALCO1.NET");
                        MailMessage mm = new MailMessage();
                        mm.From = new MailAddress("qualco.one@gmail.com");
                        mm.To.Add(NewCitizen.Email);
                        mm.Subject = "Στοιχεία δημότη " + NewCitizen.LastName + " " + NewCitizen.FirstName;
                        mm.Body = "Το username σας είναι " + NewCitizen.CitizenId + " και ο κωδικός πρόσβασης είναι " + NewCitizen.Password;
                        ss.Send(mm);
                        //Citizen update SendEmail row
                        NewCitizen.SendEmail = true;

                        // create a new AspNetUser for the citizen
                        var NewCitizenAuth = new ApplicationUser
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = TempCitizen.Vat,
                            AccessFailedCount = 0,
                            EmailConfirmed = true,
                            LockoutEnabled = true,
                            PhoneNumberConfirmed = true,
                            TwoFactorEnabled = false,
                            PasswordHash = NewCitizen.Password.GetHashCode().ToString()
                        };

                        // give the citizens' user a role
                        //var NewCitizenRole = new ApplicationRole
                        //{
                        //    RoleId = .....
                        //};
                        //_userRole.
                        _userManager.Users.Add(NewCitizenAuth);
                        _userManager.SaveChanges();

                        // example from tutorials
                        //_userManager.CreateAsync(TempCitizen.Vat, "123!@#aZ");
                        //var user = new ApplicationUser { UserName = TempCitizen.Vat, Email = TempCitizen.Vat };
                        //var result = _userManager.CreateAsync(user, "123!@#aZ");

                        // bill 
                        var NewBill = new Bill
                        {
                            Amount = TempCitizen.Amount,
                            BillDescription = TempCitizen.BillDescription,
                            BillMunicipalityId = TempCitizen.BillMunicipalityId,
                            CitizenId = TempCitizen.Vat,
                            DueDate = TempCitizen.DueDate
                        };
                        //context_.AspNetUsers.Add(NewCitizenAuth);
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
            catch (Exception ex)
            {
                // handle the error
            }
            
=======
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
>>>>>>> 0319a9afc0cf2301d18d1f186e09b3802ec68962
        }
    }
}
