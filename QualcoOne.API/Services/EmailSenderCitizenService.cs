using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace QualcoOne.API.Services
{
    public class EmailSenderCitizenService : IEmailSenderCitizenService
    {
        private readonly DataBase context_;

        public EmailSenderCitizenService(DataBase context)
        {
            context_ = context;
        }

        void IEmailSenderCitizenService.EmailSenderCitizenService()
        {

            try
            {
                //Get Citizens from Citizen where SendEmail = true
                //var Citizens = context_.Citizens;
                var Citizens = context_.Citizens
                    .Where(c => c.SendEmail == true);

                //if we have new citizen account (SendEmail == true)
                if (Citizens.Count() > 0)
                {
                    //Set Smtp
                    SmtpClient ss = new SmtpClient("smtp.gmail.com", 587);
                    ss.EnableSsl = true;
                    ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                    ss.UseDefaultCredentials = false;
                    ss.Credentials = new NetworkCredential("qualco.one@gmail.com", "QUALCO1.NET");
                    MailMessage mm = new MailMessage();
                    mm.From = new MailAddress("qualco.one@gmail.com");

                    foreach (var Citizen in Citizens)
                    {
                        mm.To.Add(Citizen.Email);
                        mm.Subject = "Στοιχεία λογαριασμού του δημότη " + Citizen.LastName + " " + Citizen.FirstName;
                        mm.Body = "Το username σας είναι " + Citizen.CitizenId + " και ο κωδικός πρόσβασης είναι " + Citizen.Password;
                        ss.Send(mm);
                        //Citizen update SendEmail row
                        Citizen.SendEmail = false;
                    }
                    //save context_ changes
                    context_.SaveChanges();
                }
            }
            catch
            {

            }
        }
    }
}
