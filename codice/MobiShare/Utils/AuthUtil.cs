namespace MobiShare.Utils
{
    public class AuthUtil
    {
        public static async Task<bool> IsAdmin(HttpContext httpContext)
        {
            var token = httpContext.Session.GetString("JWToken");
            if (token == null) return false;

            string payload= await JwtUtil.ValidateAndExtractClaimAsync(token, "role");
            return payload == "admin";
        }
    }
}
