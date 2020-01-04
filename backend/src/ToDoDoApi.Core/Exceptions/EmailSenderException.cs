using System;
using Microsoft.AspNetCore.Http;
using ToDoDoApi.Core.Helpers;

namespace ToDoDoApi.Core.Exceptions
{
    public class EmailSenderException : Exception
    {
        public int StatusCode { get; }
        public string ReasonPhrase { get; }

        public EmailSenderException()
        {
            StatusCode = StatusCodes.Status400BadRequest;
            ReasonPhrase = Constants.BadRequest;
        }

        public EmailSenderException(string message)
            : base(message)
        {
            StatusCode = StatusCodes.Status400BadRequest;
            ReasonPhrase = Constants.BadRequest;
        }

        public EmailSenderException(string message, Exception inner)
            : base(message, inner)
        {
            StatusCode = StatusCodes.Status400BadRequest;
            ReasonPhrase = Constants.BadRequest;
        }
    }
}