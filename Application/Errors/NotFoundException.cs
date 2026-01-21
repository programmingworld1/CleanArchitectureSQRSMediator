using Microsoft.AspNetCore.Http;

namespace Application.Errors
{
    internal class NotFoundException : HttpException
    {
        public string Resource { get; }
        public object Key { get; }

        public NotFoundException(string resource, object key)
            : base(StatusCodes.Status404NotFound, "NotFound", $"{resource} '{key}' was not found.")
        {
            Resource = resource;
            Key = key;
        }
    }
}
