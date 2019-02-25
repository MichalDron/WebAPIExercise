using CustomerAddressManager.BusinessDomain.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CustomerAddressManager.WebApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private static readonly Dictionary<Type, HttpStatusCode> ExceptionToHttpStatusCodeMapping = new Dictionary<Type, HttpStatusCode>
        {
            { typeof(EntityNotFoundException<>), HttpStatusCode.NotFound },
            { typeof(EntityAlreadyExistsException<>), HttpStatusCode.Conflict },
        };

        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var type = exception.GetType();

            if (type.IsGenericTypeDefinition)
            {
                var genericType = type.GetGenericTypeDefinition();

                if (ExceptionToHttpStatusCodeMapping.ContainsKey(genericType))
                {
                    code = ExceptionToHttpStatusCodeMapping[genericType];
                }
            }

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
