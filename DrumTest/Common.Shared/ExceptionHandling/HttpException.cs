using System;
using System.Net;


namespace Common.Shared.ExceptionHandling
{
    public class HttpException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }
        public string StatusCode { get; }
        public object CustomData { get; }
        public bool HasStatusCode => !string.IsNullOrEmpty(StatusCode);

        public HttpException(string message, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
            : this(message, null, httpStatusCode)
        {
        }

        public HttpException(string message, string statusCode, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
            StatusCode = statusCode;
        }

        public HttpException(string message, string statusCode, HttpStatusCode httpStatusCode, object customData)
            : this(message, statusCode, httpStatusCode)
        {
            CustomData = customData;
        }
    }
}
