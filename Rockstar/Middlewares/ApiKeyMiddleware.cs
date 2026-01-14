namespace Rockstar.Middlewares
{
    public class ApiKeyMiddleware
    {
        private const string HeaderName = "X-Api-Key";
        private readonly RequestDelegate _next;
        private readonly string[] _acceptedKeys;

        public ApiKeyMiddleware(
            RequestDelegate next, 
            IConfiguration configuration)
        {
            _next = next;
            _acceptedKeys = configuration
                .GetSection("AcceptedApiKeys")
                .Get<string[]>() ?? Array.Empty<string>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(HeaderName, out var apiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Missing API key");
                return;
            }

            if (!_acceptedKeys.Contains(apiKey.ToString()))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid API key");
                return;
            }

            await _next(context);
        }
    }
}
