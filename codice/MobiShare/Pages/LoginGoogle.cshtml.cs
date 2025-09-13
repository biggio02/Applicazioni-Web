using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MobiShare.Pages
{
    public class LoginGoogleModel : PageModel
    {
        public IActionResult OnGet()
        {
            
            
            // Parametri per il login Google OAuth 2.0
            var clientId = "824952070974-egfjjapdh126o1c6359k842v1cu3llns.apps.googleusercontent.com";
            var redirectUri = Url.Page("GoogleCallback", null, null, Request.Scheme); // pagina per gestire callback
            var scope = "openid email profile";
            var responseType = "code";
            // valore random per il CSRF
            var state = Random.Shared.Next(100, 10000000).ToString();
            HttpContext.Session.SetString("state",state);

            var queryParams = HttpUtility.ParseQueryString(string.Empty);
            queryParams["client_id"] = clientId;
            queryParams["redirect_uri"] = redirectUri;
            queryParams["response_type"] = responseType;
            queryParams["scope"] = scope;
            queryParams["state"] = state;

            var authorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
            var authorizationUrl = $"{authorizationEndpoint}?{queryParams}";

            // Redirect all'endpoint di Google
            return Redirect(authorizationUrl);
        }
    }
}
