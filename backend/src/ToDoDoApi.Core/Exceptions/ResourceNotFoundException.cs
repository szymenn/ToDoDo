using System;
using Microsoft.AspNetCore.Http;
using ToDoDoApi.Core.Helpers;

namespace ToDoDoApi.Core.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public int StatusCode { get; }
        public string ReasonPhrase { get;  }
        
        public ResourceNotFoundException()
        {
            StatusCode = StatusCodes.Status404NotFound;
            ReasonPhrase = Constants.NotFound;
        }
        
        public ResourceNotFoundException(string message)
            : base(message)
        {
            StatusCode = StatusCodes.Status404NotFound;
            ReasonPhrase = Constants.NotFound;
        }

        public ResourceNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
            StatusCode = StatusCodes.Status404NotFound;
            ReasonPhrase = Constants.NotFound;
        }

    }
}