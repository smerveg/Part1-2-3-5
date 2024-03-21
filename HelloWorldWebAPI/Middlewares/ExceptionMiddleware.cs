using HelloWorldWebAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace HelloWorldWebAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
        {
            _next=next;
            _logger = logger;
           
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                var response = context.Response;
                response.ContentType= "application/json";

                switch (ex)
                {
                    case NotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case BadRequestException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                _logger.LogError(ex.Message);
                context.Response.StatusCode = response.StatusCode;
                var result=JsonSerializer.Serialize(new { error = ex.Message });
                await context.Response.WriteAsync(result);
            }
            
        }
    }
}
