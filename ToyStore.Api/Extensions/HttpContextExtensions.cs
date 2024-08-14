using System.Security.Claims;

namespace ToyStore.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static string getCurrentUserIdentifier(this HttpContext context)
        {
            return context.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value??" ";
        }

        public static string getCurrentUserEmail(this HttpContext context)
        {
            return context.User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value ?? " ";
        }
        public static string getCurrentUserRole(this HttpContext context)
        {
            return context.User.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault()?.Value ?? " ";
        }
    }
}
