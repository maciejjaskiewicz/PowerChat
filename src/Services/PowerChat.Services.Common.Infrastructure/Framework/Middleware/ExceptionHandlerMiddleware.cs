using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PowerChat.Services.Common.Application.Exceptions;
using PowerChat.Services.Common.Infrastructure.Framework.Models;

namespace PowerChat.Services.Common.Infrastructure.Framework.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorCode = "error";
            HttpStatusCode statusCode;
            object result = "Something went wrong!";

            var exceptionType = exception.GetType();

            switch (exception)
            {
                case UnauthorizedAccessException _ when exceptionType == typeof(UnauthorizedAccessException):
                    result = "Unauthorized access.";
                    statusCode = HttpStatusCode.Unauthorized;
                    break;

                case PowerChatValidationException e when exceptionType == typeof(PowerChatValidationException):
                    result = e.Failures;
                    statusCode = HttpStatusCode.BadRequest;
                    errorCode = e.Code;
                    break;

                case PowerChatApplicationException e when exceptionType == typeof(PowerChatApplicationException):
                    result = e.Message;
                    statusCode = HttpStatusCode.BadRequest;
                    errorCode = e.Code;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            var response = new ExceptionResponseModel
            {
                Code = errorCode,
                Result = result
            };

            var payload = JsonConvert.SerializeObject(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) statusCode;

            return context.Response.WriteAsync(payload);
        }
    }
}
