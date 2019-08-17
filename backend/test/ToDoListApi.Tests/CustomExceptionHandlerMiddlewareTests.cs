using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using ToDoListApi.Exceptions;
using ToDoListApi.Extensions;
using Xunit;

namespace ToDoListApi.Tests
{
    public class CustomExceptionHandlerMiddlewareTests
    {
        [Fact]
        public async Task Invoke_WhenResourceNotFoundException_SetsNotFoundStatusCode()
        {
            var middleware = new CustomExceptionHandlerMiddleware
                (context => throw new ResourceNotFoundException());

            var httpContext = new DefaultHttpContext();
            await middleware.Invoke(httpContext);
            
            Assert.Equal(StatusCodes.Status404NotFound, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task Invoke_WhenResourceAlreadyExistsException_SetsConflictStatusCode()
        {
            var middleware = new CustomExceptionHandlerMiddleware
                (context => throw new ResourceAlreadyExistsException());
            
            var httpContext = new DefaultHttpContext();
            await middleware.Invoke(httpContext);

            Assert.Equal(StatusCodes.Status409Conflict, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task Invoke_WhenRegistrationException_SetsBadRequestStatusCode()
        {
            var middleware = new CustomExceptionHandlerMiddleware
                (context => throw new RegistrationException());
            
            var httpContext = new DefaultHttpContext();
            await middleware.Invoke(httpContext);
            
            Assert.Equal(StatusCodes.Status400BadRequest, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task Invoke_WhenPasswordValidationException_SetsBadRequestStatusCode()
        {
            var middleware = new CustomExceptionHandlerMiddleware
                (context => throw new PasswordValidationException());
            
            var httpContext = new DefaultHttpContext();
            await middleware.Invoke(httpContext);
            
            Assert.Equal(StatusCodes.Status400BadRequest, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task Invoke_WhenResourceNotFoundException_WritesProblemDetails()
        {
            var middleware = new CustomExceptionHandlerMiddleware
                (context => throw new ResourceNotFoundException());
            
            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            await middleware.Invoke(httpContext);
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var jsonString = new StreamReader(httpContext.Response.Body).ReadToEnd();
            var result = JsonConvert.DeserializeObject<ProblemDetails>(jsonString);

            Assert.IsType<ProblemDetails>(result);
        }

        [Fact]
        public async Task Invoke_WhenResourceAlreadyExistsException_WritesProblemDetails()
        {
            var middleware = new CustomExceptionHandlerMiddleware
                (context => throw new ResourceAlreadyExistsException());
            
            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            await middleware.Invoke(httpContext);
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var jsonString = new StreamReader(httpContext.Response.Body).ReadToEnd();
            var result = JsonConvert.DeserializeObject<ProblemDetails>(jsonString);

            Assert.IsType<ProblemDetails>(result);
        }

        [Fact]
        public async Task Invoke_WhenRegistrationException_WritesProblemDetails()
        {
            var middleware = new CustomExceptionHandlerMiddleware
                (context => throw new RegistrationException());
             
            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            await middleware.Invoke(httpContext);
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var jsonString = new StreamReader(httpContext.Response.Body).ReadToEnd();
            var result = JsonConvert.DeserializeObject<ProblemDetails>(jsonString);

            Assert.IsType<ProblemDetails>(result);
        }
        
        [Fact]
        public async Task Invoke_WhenPasswordValidationException_WritesProblemDetails()
        {
            var middleware = new CustomExceptionHandlerMiddleware
                (context => throw new PasswordValidationException());
            
            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            await middleware.Invoke(httpContext);
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var jsonString = new StreamReader(httpContext.Response.Body).ReadToEnd();
            var result = JsonConvert.DeserializeObject<ProblemDetails>(jsonString);

            Assert.IsType<ProblemDetails>(result);
        }
        
    }
}