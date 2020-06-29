using System.Net;


namespace Common.Shared.ExceptionHandling
{
    public class BadRequestException : HttpException
    {
        public static readonly string DUPLICATE_DRUM_NB = "DUPLICATE_DRUM_NB";
        public static readonly string DRUM_DOESNT_EXISTS = "DRUM_DOESNT_EXISTS";
        public static readonly string WRONG_DRUM_MANAGER_NAME = "WRONG_DRUM_MANAGER_NAME";
        public static readonly string WRONG_SITE_NAME = "WRONG_SITE_NAME";
        public static readonly string WRONG_STATUS_NAME = "WRONG_STATUS_NAME";
        public static readonly string DUPLICATE_SITE_NAME = "DUPLICATE_SITE_NAME";

        public BadRequestException(string message)
            : base(message, HttpStatusCode.BadRequest) { }

        public BadRequestException(string message, string statusCode)
            : base(message, statusCode, HttpStatusCode.BadRequest) { }

        public BadRequestException(string message, string statusCode, object customData)
            : base(message, statusCode, HttpStatusCode.BadRequest, customData) { }
    }
}
