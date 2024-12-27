using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityGateApv.Domain.Errors;

namespace SecurityGateApv.Domain.Shared
{
    public class Result<TResult> : Result
    {
        private readonly TResult _value;
        protected internal Result(TResult? result, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _value = result;
        }
        public TResult Value => IsSuccess ? _value
            : throw new InvalidOperationException("No value for error");
        public static implicit operator Result<TResult>(TResult value) => new(value, true, Error.None);
    }
}
