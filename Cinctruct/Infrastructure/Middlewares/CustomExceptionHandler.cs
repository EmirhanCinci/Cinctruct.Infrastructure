using FluentValidation;
using Infrastructure.Constants;
using Infrastructure.CrossCuttingConcerns.Exceptions;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Infrastructure.Middlewares
{
    public static class CustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    List<string> errorMessages = new List<string>();
                    var statusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    switch (exceptionFeature?.Error)
                    {
                        case NoContentException:
                            statusCode = StatusCodes.Status204NoContent;
                            errorMessages.Add(exceptionFeature.Error.Message);
                            break;
                        case ArgumentException:
                            statusCode = StatusCodes.Status400BadRequest;
                            errorMessages.Add(exceptionFeature.Error.Message);
                            break;
                        case BadRequestException:
                            statusCode = StatusCodes.Status400BadRequest;
                            errorMessages.Add(exceptionFeature.Error.Message);
                            break;
                        case BusinessRuleException:
                            statusCode = StatusCodes.Status400BadRequest;
                            errorMessages.Add(exceptionFeature.Error.Message);
                            break;
                        case ValidationException:
                            statusCode = StatusCodes.Status400BadRequest;
                            var errors = exceptionFeature.Error.Message.Split("-");
                            foreach (var error in errors)
                            {
                                errorMessages.Add(error.Trim());
                            }
                            break;
                        case NotFoundException:
                            statusCode = StatusCodes.Status404NotFound;
                            errorMessages.Add(exceptionFeature.Error.Message);
                            break;
                        case InvalidOperationException:
                            statusCode = StatusCodes.Status422UnprocessableEntity;
                            errorMessages.Add(exceptionFeature.Error.Message);
                            break;
                        default:
                            statusCode = StatusCodes.Status500InternalServerError;
                            errorMessages.Add(SystemMessages.InternalServerError);
                            break;
                    }
                    context.Response.StatusCode = statusCode;
                    var response = CustomApiResponse<NoData>.Fail(statusCode, errorMessages);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
