using System;
using Microsoft.AspNetCore.Http;
using ToDoListApi.Helpers;

namespace ToDoListApi.Exceptions
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