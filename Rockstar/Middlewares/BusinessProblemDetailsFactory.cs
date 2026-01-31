using Application.ApplicationResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Rockstar.Middlewares
{
    public sealed class BusinessProblemDetailsFactory
    {
        private readonly ProblemDetailsFactory _factory;

        public BusinessProblemDetailsFactory(ProblemDetailsFactory factory)
        {
            _factory = factory;
        }

        public ProblemDetails Create(HttpContext httpContext, Error error)
        {
            var statusCode = MapStatusCode(error.Code);

            var problem = _factory.CreateProblemDetails(
                httpContext: httpContext,
                statusCode: statusCode,
                title: error.Code,
                detail: error.Message,
                type: $"https://httpstatuses.com/{statusCode}",
                instance: httpContext.Request.Path
            );

            problem.Extensions["traceId"] = httpContext.TraceIdentifier;

            return problem;
        }

        private static int MapStatusCode(string errorCode)
            => errorCode switch
            {
                "EmailAlreadyExists" => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status400BadRequest
            };
    }
}
