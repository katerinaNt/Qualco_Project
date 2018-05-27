using QualcoOne.API.ViewModels;
using QualcoOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QualcoOne.API.Data;

namespace QualcoOne.API.Services
{
    public class CitizenAccountService : ICitizenAccountService
    {
        private readonly DataBase context_;

        public CitizenAccountService(DataBase context)
        {
            context_ = context;
        }

        public async Task<APIResult<Citizen>> GetCitizenAccountAsync(
            string LoginAccountId)
        {

            if (string.IsNullOrWhiteSpace(LoginAccountId))
            {
                return APIResult<Citizen>.CreateFailed(
                    HttpStatusCode.BadRequest, $"Null or empty {nameof(LoginAccountId)}");
            }

            if (LoginAccountId.Length != 9)
            {
                return APIResult<Citizen>.CreateFailed(
                    HttpStatusCode.BadRequest, $"Invalid {nameof(LoginAccountId)}");
            }

            var citizen = await context_.Citizens
                .Where(c => c.CitizenId.Equals(LoginAccountId,
                    StringComparison.OrdinalIgnoreCase))
                .SingleOrDefaultAsync();

            if (citizen == null)
            {
                return APIResult<Citizen>.CreateFailed(
                    HttpStatusCode.NotFound, $"Invalid {nameof(LoginAccountId)}");
            }

            return APIResult<Citizen>.CreateSuccessful(citizen);   
        }




        public async Task<APIResult<Citizen>> UpdateCitizenPassword(string LoginAccountId, string TypedCurrentPassword, string NewPassword, string VerifyNewPassword)
        {
            if (string.IsNullOrWhiteSpace(LoginAccountId))
            {
                return APIResult<Citizen>.CreateFailed(
                    HttpStatusCode.BadRequest, $"Null or empty {nameof(LoginAccountId)}");
            }

            if (LoginAccountId.Length != 9)
            {
                return APIResult<Citizen>.CreateFailed(
                    HttpStatusCode.BadRequest, $"Invalid {nameof(LoginAccountId)}");
            }

            var citizen = await context_.Citizens
                .Where(c => c.CitizenId.Equals(LoginAccountId,
                    StringComparison.OrdinalIgnoreCase))
                .Where(c => c.Password == TypedCurrentPassword)
                .SingleOrDefaultAsync();

            if (citizen == null)
            {
                return APIResult<Citizen>.CreateFailed(
                    HttpStatusCode.NotFound, $"Invalid {nameof(LoginAccountId)}");
            }

            citizen.Password = NewPassword;
            citizen.ChangePassword = false;

            context_.Citizens.Update(citizen);
            context_.SaveChanges();

            return APIResult<Citizen>.CreateSuccessful(citizen);
        }
    }
}

