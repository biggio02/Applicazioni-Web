using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobiShare.Models;
using MobiShare.Services;

namespace MobiShare.Pages
{
    public class GoogleCallbackModel : PageModel
    {
        public Token token { get; set; }
        private readonly ITokenService _tokenService;
        public GoogleCallbackModel(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public async Task<IActionResult> OnGet(string code, string state, string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                // L'utente ha rifiutato o c'è stato un errore
                Console.WriteLine($"Errore durante login Google: {error}");
                return Redirect("/Login");
            }

            if (string.IsNullOrEmpty(code))
            {
                Console.WriteLine("Codice di autorizzazione mancante");
                return Redirect("/Login");
            }

            if (HttpContext.Session.GetString("state") != state)
            {
                Console.WriteLine("Parametro State Invalido");
                return Redirect("/Login");
            }

            token =await _tokenService.getTokenGoogle(code);
            Console.WriteLine("Token ricevuto: " + token.token);
            if (token.token == null)
            {
                Console.WriteLine("token vuoto");
                TempData["Message"] = "Account Non trovato";
                return RedirectToPage("Login");
            }
            else
            {
                HttpContext.Session.SetString("JWToken", token.token);
                Console.WriteLine("Token: " + token.token);
                return RedirectToPage("Account");
            }
        }
    }
}
