using System;
using System.Collections.Generic;
using System.Net;

using Amazon.DynamoDBv2.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

using AcidLabsTest.Service.Models.Exceptions;
using AcidLabsTest.Service.Frameworks.DataReadOnly;

namespace AcidLabsTest.Api.Frameworks
{
    internal static class CustomHandlingException
    {
        internal static IApplicationBuilder UseCustomHandlingException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;
                var exName = exception.GetType().Name;
                var statusCode = (int) HttpStatusCode.InternalServerError;

                if (exception is not null && !string.IsNullOrEmpty(exName))
                {
                    statusCode = GetHttpStatusCode(exName);
                }

                context.Response.ContentType = ContentTypeData.ApplicationJson;
                context.Response.StatusCode = statusCode;
                var exceptionMessage = JsonConvert.SerializeObject(new ExceptionModel(exception.GetBaseException().GetType().Name, exception.Message, statusCode));
                await context.Response.WriteAsync(exceptionMessage).ConfigureAwait(false);
            }));

            return app;
        }

        private static int GetHttpStatusCode(string name)
        {
            return name switch {
                nameof(ArgumentNullException) => (int) HttpStatusCode.BadRequest,
                nameof(ResourceInUseException) => (int) HttpStatusCode.Forbidden,
                nameof(FieldAccessException) => (int) HttpStatusCode.Forbidden,
                nameof(KeyNotFoundException) => (int) HttpStatusCode.NotFound,
                nameof(UnauthorizedAccessException) => (int) HttpStatusCode.Unauthorized,
                _ => (int) HttpStatusCode.InternalServerError,
            };
        }
    }
}
