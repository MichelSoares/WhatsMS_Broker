using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsMS_Broker.Application.Common.Results
{
    namespace WhatsMS_Broker.Application.Common.Results
    {
        public class Result<T>
        {
            public T Data { get; }
            public bool IsSuccess { get; }
            public string ErrorMessage { get; }
            public string ErrorCode { get; }
            public int StatusCode { get; }

            protected Result(T data, bool isSuccess, string errorMessage, string errorCode, int statusCode)
            {
                Data = data;
                IsSuccess = isSuccess;
                ErrorMessage = errorMessage;
                ErrorCode = errorCode;
                StatusCode = statusCode;
            }

            public static Result<T> Success(T data, int statusCode = 200) => new Result<T>(data, true, null, null, statusCode);
            public static Result<T> Failure(string errorMessage, string errorCode = null, int statusCode = 400) => new Result<T>(default(T), false, errorMessage, errorCode, statusCode);
            public static Result<T> NotFound(string errorMessage = "Recurso não encontrado", string errorCode = "NOT_FOUND", int statusCode = 404) => new Result<T>(default(T), false, errorMessage, errorCode, statusCode);
        }

        public class Result : Result<bool>
        {
            protected Result(bool isSuccess, string errorMessage, string errorCode, int statusCode)
                : base(default(bool), isSuccess, errorMessage, errorCode, statusCode) { }

            public static Result Success(int statusCode = 200) => new Result(true, null, null, statusCode);
            public static new Result Failure(string errorMessage, string errorCode = null, int statusCode = 400) => new Result(false, errorMessage, errorCode, statusCode);
            public static new Result NotFound(string errorMessage = "Recurso não encontrado", string errorCode = "NOT_FOUND", int statusCode = 404) => new Result(false, errorMessage, errorCode, statusCode);
        }
    }
}
