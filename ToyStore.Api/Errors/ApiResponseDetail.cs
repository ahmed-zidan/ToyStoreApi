namespace ToyStore.Api.Errors
{
    public class ApiResponseDetail:ApiResponse
    {
        public string Detail { get; set; }
        public ApiResponseDetail() : base(400)
        {

        }
        public ApiResponseDetail(int statusCode , string message , string detail):base(statusCode, message){
            Detail = detail;
        }
    }
}
