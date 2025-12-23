using Microsoft.AspNetCore.Http;

namespace Application.Errors
{
    public class DuplicateEmailException : HttpException
    {
        public DuplicateEmailException(string message)
            : base(StatusCodes.Status409Conflict, message) { }
    }
}
