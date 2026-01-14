using Microsoft.AspNetCore.Http;

namespace Application.Errors
{
    public class WrongEmailException : HttpException
    {
        public WrongEmailException(string message)
            : base(StatusCodes.Status409Conflict, message) { }
    }
}
