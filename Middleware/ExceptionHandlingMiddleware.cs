using System.Net;
using ASP.NET_Core_REST_API.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace ASP.NET_Core_REST_API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, title) = exception switch
            {
                BadRequestException => (StatusCodes.Status400BadRequest, "Business Rule Violation"),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
            };

            if (statusCode == StatusCodes.Status400BadRequest)
            {
                _logger.LogWarning(
                    exception,
                    "Business rule violation for {Method} {Path}: {Message}",
                    context.Request.Method,
                    context.Request.Path,
                    exception.Message
                );
            }
            else
            {
                _logger.LogError(
                    exception,
                    "Unhandled exception occurred for {Method} {Path}. User: {User}, TraceId: {TraceId}",
                    context.Request.Method,
                    context.Request.Path,
                    context.User?.Identity?.Name ?? "Anonymous",
                    context.TraceIdentifier
                );
            }

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = statusCode == StatusCodes.Status500InternalServerError 
                    ? "An internal server error occurred. Please contact support." 
                    : exception.Message,
                Instance = context.Request.Path,
                Extensions =
                {
                    ["traceId"] = context.TraceIdentifier
                }
            };

            return context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}