using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameInfoService.Rating.App.Middleware.ExceptionHandling.ExceptionHandlers;
using Microsoft.AspNetCore.Builder;

namespace GameInfoService.Rating.App.Middleware.ExceptionHandling
{
    public static class ExceptionHandleMiddlewareExtension
    {
        public static void UseExceptionHandleMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandleMiddleware>();
        }
    }
}
