using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobiShare.Models;
using MobiShare.Services;
using MobiShare.Utils;

namespace MobiShare.Pages
{
    public class UtentiModel : PageModel
    {
        private readonly IAccountService _accountService;
        public List<Utente> Utenti { get; set; }
        public string Username { get; set; }
        [BindProperty(SupportsGet = true)]
        public string UtenteBloccato { get; set; }
        [BindProperty(SupportsGet = true)]
        public int limit { get; set; } = 10;
        [BindProperty(SupportsGet = true)]
        public int offset { get; set; } = 0;
        public UtentiModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<IActionResult> OnGet()
        {

            string token = HttpContext.Session.GetString("JWToken");
            if (!await AuthUtil.IsAdmin(HttpContext))
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }
            Username = await JwtUtil.ValidateAndExtractClaimAsync(token, "username");

            Utenti = await _accountService.getAllAccount(token,$"?offset={offset}&limit={limit}&utenteBloccato={UtenteBloccato}");

            return Page();

        }

        public async Task<IActionResult> OnPost(string UsernameUtente)
        {
            string token = HttpContext.Session.GetString("JWToken");
            if (token == null)
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }
            await _accountService.patchUtente(UsernameUtente,token);
            return Redirect("/Utenti");
        }
    }
}
