using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using GameInfoService.Rating.App.Middleware.ExceptionHandling.CustomExceptions;
using GameInfoService.Rating.App.Middleware.ExceptionHandling.ExceptionHandleResponses;

namespace GameInfoService.Rating.App.Middleware.ExceptionHandling.ExceptionHandlers
{
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InternalException ex)
            {
                await HandleException(context, ex);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex switch
            {
                KeyNotFoundException => (int) HttpStatusCode.BadRequest,
                _ => (int) HttpStatusCode.InternalServerError,
            };
            //var message = ex switch
            //{
            //    KeyNotFoundException => "Object not found",
            //    _ => "Unexpected error"
            //};
            await context.Response.WriteAsync(new ExceptionHandleDefaultResponse
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message
            }.ToString());

        }

    }
}
