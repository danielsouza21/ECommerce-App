namespace API.Services.ErrorHandlers
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "You made a bad request.",
                401 => "You are not Authorized.",
                404 => "Resource was not found.",
                500 => "Generic internal error, related to the server / application.",
                _ => null  //default
            };
        }
    }
}
