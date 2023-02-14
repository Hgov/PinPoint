namespace PinPoint.API.ApiException
{
    public class ApiException : Exception
    {
        internal ApiException(string businessMessage)
            : base(businessMessage)
        {
        }

        internal ApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
