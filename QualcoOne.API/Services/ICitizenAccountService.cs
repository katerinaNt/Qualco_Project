using QualcoOne.API.ViewModels;
using QualcoOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualcoOne.API.Services
{
    public interface ICitizenAccountService
    {
        Task<APIResult<Citizen>> GetCitizenAccountAsync(string LoginAccountId);
        Task<APIResult<Citizen>> UpdateCitizenPassword(string LoginAccountId,
            string TypedCurrentPassword, string NewPassword, string VerifyNewPassword);

    }
}
