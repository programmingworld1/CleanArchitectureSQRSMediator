namespace Application.Errors
{
    public abstract class HttpException : Exception
    {
        public int StatusCode { get; }
        public string ErrorCode { get; }

        protected HttpException(int statusCode, string errorCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }
}
