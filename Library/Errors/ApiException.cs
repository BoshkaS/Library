namespace Library.Errors
{
    public class ApiException
    {
        public ApiException(int status, string message, string details)
        {
            StatusCode = status;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public string Details { get; set; }
    }
}
