using System;
using Microsoft.AspNetCore.Http;
using ToDoListApi.Helpers;

namespace ToDoListApi.Exceptions
{
    public class PasswordValidationException : Exception
    {
        public int StatusCode { get; }
        public string ReasonPhrase { get; }

        public PasswordValidationException()
        {
            StatusCode = StatusCodes.Status400BadRequest;
            ReasonPhrase = Constants.BadRequest;
        }

        public PasswordValidationException(string message)
            : base(message)
        {
            StatusCode = StatusCodes.Status400BadRequest;
            ReasonPhrase = Constants.BadRequest;
        }

        public PasswordValidationException(string message, Exception inner)
            : base(message, inner)
        {
            StatusCode = StatusCodes.Status400BadRequest;
            ReasonPhrase = Constants.BadRequest;
        }
    }
}