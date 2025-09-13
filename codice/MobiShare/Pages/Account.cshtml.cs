using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobiShare.Models;
using MobiShare.Services;
using MobiShare.Utils;

namespace MobiShare.Pages
{
    public class AccountModel : PageModel
    {
        private readonly IAccountService _accountService;
        public Utente Utente { get; set; }
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public AccountModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JWToken");
            if (token==null)
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }
            var username = await JwtUtil.ValidateAndExtractClaimAsync(token,"username");
            Console.WriteLine("username da account: " + username);
           
            Utente = await _accountService.getAccount(username, token);
            if (Utente.Email == null)
            {
                Console.WriteLine("account non trovato: "+username);
                return Redirect("/Index");
            }
            return Page();
        
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string token = HttpContext.Session.GetString("JWToken");
            Username = await JwtUtil.ValidateAndExtractClaimAsync(token, "username");
            if (token == null)
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }
            Messaggio m = await _accountService.putUtente(token,Password);
            if (m.message == null)
            {
                Console.WriteLine("Errore cambio password");
            }
            else
            {
                Console.WriteLine("Cambio password completato");
            }
            return Redirect("/Account");
        }
    }
}
