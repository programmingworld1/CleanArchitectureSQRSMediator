namespace Application.Errors
{
    public class DuplicateEmailException : HttpException
    {
        public DuplicateEmailException(string message)
            : base("EmailAlreadyExists", message) { }
    }
}
