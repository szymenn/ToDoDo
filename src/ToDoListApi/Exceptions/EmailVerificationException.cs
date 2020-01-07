using System;
using Microsoft.AspNetCore.Http;
using ToDoListApi.Helpers;

namespace ToDoListApi.Exceptions
{
    public class EmailVerificationException : Exception
    {
        public int StatusCode { get; }
        public string ReasonPhrase { get; }

        public EmailVerificationException()
        {
            StatusCode = StatusCodes.Status400BadRequest;
            ReasonPhrase = Constants.BadRequest;
        }

        public EmailVerificationException(string message)
            : base(message)
        {
            StatusCode = StatusCodes.Status400BadRequest;
            ReasonPhrase = Constants.BadRequest;
        }

        public EmailVerificationException(string message, Exception inner)
            : base(message, inner)
        {
            StatusCode = StatusCodes.Status400BadRequest;
            ReasonPhrase = Constants.BadRequest;
        }

    }
}