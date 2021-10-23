using Microsoft.AspNetCore.Mvc;

namespace CodeEditorApi.Errors
{
    public class ApiError
    {
        public int StatusCode { get; private set; }

        public string StatusDescription { get; private set; }

        public string Message { get; private set; }

        public ApiError(int statusCode, string statusDescription)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
        }

        public ApiError(int statusCode, string statusDescription, string message)
            : this(statusCode, statusDescription)
        {
            Message = message;
        }

        public static ActionResult BadRequest(string message)
        {
            return new BadRequestObjectResult(new BadRequestError(message));
        }
    }
}
