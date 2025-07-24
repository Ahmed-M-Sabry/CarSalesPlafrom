using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string Error { get; }
        public ErrorType ErrorType { get; }

        private Result(T value)
        {
            IsSuccess = true;
            Value = value;
            Error = null;
            ErrorType = ErrorType.None;
        }

        private Result(string error, ErrorType errorType)
        {
            IsSuccess = false;
            Value = default;
            Error = error;
            ErrorType = errorType;
        }

        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Failure(string error, ErrorType errorType) => new Result<T>(error, errorType);
    }

    public enum ErrorType
    {
        None,
        NotFound,
        BadRequest,
        Conflict,
        UnprocessableEntity,
        InternalServerError
    }
}