using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GameInfoService.Rating.App.Middleware.ExceptionHandling.CustomExceptions;

namespace GameInfoService.Rating.App.Middleware.ExceptionHandling.ExceptionHandlers
{
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (CustomException ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = ex.Message;
                response.StatusCode = (int)HttpStatusCode.Conflict;
                await response.WriteAsync(responseModel);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                const string responseModel = "I am response";
                response.StatusCode = ex switch
                {
                    KeyNotFoundException => (int) HttpStatusCode.NotFound,
                    _ => response.StatusCode
                };
                await response.WriteAsync(responseModel);
            }
        }

    }
}
