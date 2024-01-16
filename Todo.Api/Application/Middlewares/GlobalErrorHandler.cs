using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Todo.Api.Application.Middlewares
{
    public enum ErrorType
    {
        GeneralError,
        DatabaseError,
    }

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var errorType = MapErrorType(exception);

            switch (errorType)
            {
                case ErrorType.DatabaseError:
                    code = HttpStatusCode.NotFound;
                    break;
                default:
                    break;
            }

            var result = JsonConvert.SerializeObject(new { error = errorType.ToString(), message = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private static ErrorType MapErrorType(Exception exception)
        {
            if (exception is DbUpdateException dbUpdateException) return ErrorType.DatabaseError;

            return ErrorType.GeneralError;
        }
    }

}
