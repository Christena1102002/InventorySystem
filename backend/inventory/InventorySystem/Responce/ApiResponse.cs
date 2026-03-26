namespace InventorySystem.API.Responce
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public ApiResponse(int statusCode, string? message = null, object? data = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            Data = data;
        }
        private string? GetDefaultMessageForStatusCode(int? statusCode)
        {
            return statusCode switch
            {
                200 => "OK",
                400 => "Bad Request",
                401 => "You Are Not Authorized",
                404 => "Resource Not Found",
                409 => "Phone Number is Not Verified",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}
