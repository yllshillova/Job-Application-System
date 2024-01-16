using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Core
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public ResultErrorType ErrorType { get; set; }   
        public static Result<T> Success(T value) => new Result<T>{IsSuccess = true, Value = value};
        public static Result<T> Failure(ResultErrorType errorType) => new Result<T> {IsSuccess = false, ErrorType = errorType};
    }
}