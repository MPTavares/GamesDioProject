using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GamesDIOApi.Middleware;

namespace MoviesDIOApi.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        public ExceptionMiddleware()
        {

        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await ExceptionHandlerAsync(context,ex);
            }
        }

        private async Task ExceptionHandlerAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var result = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "Ocorreu um erro",
                InternalMessage =$"{ex.Message}"
            };
            await context.Response.WriteAsync(result.ToString());
     
        }
    }
}
