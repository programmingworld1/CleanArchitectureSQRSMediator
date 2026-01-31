namespace Application.Errors
{
    public class WrongEmailException : HttpException
    {
        public WrongEmailException(string message)
            : base("WrongEmail", message) { }
    }
}
