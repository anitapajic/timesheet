using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using TimesheetApp.Application.Common.Exceptions;

namespace TimesheetApp.Extensions;

public static class ErrorHandlerExtension
{
    public static void UseErrorHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature == null) return;

                context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = contextFeature.Error switch
                {
                    BadRequestException => (int)HttpStatusCode.BadRequest,
                    OperationCanceledException => (int)HttpStatusCode.ServiceUnavailable,
                    NoDataFoundException => (int)HttpStatusCode.NotFound,
                    EntityAlreadyExistsException => (int)HttpStatusCode.Conflict,
                    DateOutOfRangeException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var errorResponse = new
                {
                    statusCode = context.Response.StatusCode,
                    message = contextFeature.Error.Message,
                    errors = (contextFeature.Error as BadRequestException)?.Errors 
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            });
        });
    }
}