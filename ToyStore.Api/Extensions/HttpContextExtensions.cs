using System.Security.Claims;

namespace ToyStore.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static string getCurrentUserIdentifier(this HttpContext context)
        {
            return context.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value??" ";
        }
    }
}
