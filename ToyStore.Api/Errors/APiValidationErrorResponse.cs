namespace ToyStore.Api.Errors
{
    public class APiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string>Errors { get; set; }
        public APiValidationErrorResponse() : base(400)
        {

        }
        public APiValidationErrorResponse(int code, string message, List<string> errors) : base(code, message)
        {
            Errors = errors;
        }
    }
}
