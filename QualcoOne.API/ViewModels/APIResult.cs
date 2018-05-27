using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QualcoOne.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace QualcoOne.API.ViewModels
{
    public class APIResult<T>
    {
        public int ErrorCode { get; set; }
        public string ErrorText { get; set; }
        public T Data { get; set; }

        public static APIResult<T> CreateFailed(
            HttpStatusCode errorCode, string errorText)
        {
            return new APIResult<T>()
            {
                ErrorCode = (int)errorCode,
                ErrorText = errorText
            };
        }

        public static APIResult<T> CreateSuccessful(T data)
        {
            return new APIResult<T>()
            {
                ErrorCode = (int)HttpStatusCode.OK,
                Data = data
            };
        }

        public bool IsSuccess()
        {
            return ErrorCode == (int)HttpStatusCode.OK;
        }
    }
}
