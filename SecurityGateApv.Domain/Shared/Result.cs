using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityGateApv.Domain.Errors;

namespace SecurityGateApv.Domain.Shared
{
    public class Result
    {
        protected internal Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }
            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException();
            }
            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; set; }
        public Error Error { get; set; }
        public bool IsFailure => !IsSuccess;
        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);
        public static Result<TResult> Success<TResult>(TResult result) => new(result, true, Error.None);
        public static Result<TResult> Failure<TResult>(Error error) => new(default, false, error);
    }
}
