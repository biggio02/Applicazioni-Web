using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobiShare.Models;
using MobiShare.Services;
using MobiShare.Utils;
using FluentValidation.Results;
using FluentValidation;

namespace MobiShare.Pages
{
    public class RicaricaModel : PageModel
    {
        private readonly IValidator<CartaCredito> _validator;
        [BindProperty]
        public CartaCredito Input { get; set; } = new();

        [BindProperty]
        public string codice { get; set; }

        public Ricarica r { get; set; }
        public string Username { get; set; }
        private readonly IAccountService _accountService;
        public RicaricaModel(IAccountService accountService, IValidator<CartaCredito> validator)
        {
            _accountService = accountService;
            _validator = validator;
        }
        public async Task<IActionResult> OnPostCartaCredito()
        {
            ValidationResult result = await _validator.ValidateAsync(Input);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError($"Input.{error.PropertyName}", error.ErrorMessage);
                }
                return Page();
            }

            string token = HttpContext.Session.GetString("JWToken");
            if (token == null)
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }
            Username = await JwtUtil.ValidateAndExtractClaimAsync(token, "username");
            r = await _accountService.postRicaricaCarta(Input.Importo,Input.Numero,Input.Scadenza,Input.Cvc,token);
            if (r == null)
            {
                Console.WriteLine("errore nella ricarica");
            }
            return RedirectToPage("Account");

        }

        public async Task<IActionResult> OnPostCartaPrepagata()
        {
            string token = HttpContext.Session.GetString("JWToken");
            if (token == null)
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }
            Username = await JwtUtil.ValidateAndExtractClaimAsync(token, "username");
            r = await _accountService.postRicaricaPrepagata(token,codice);
            if (r.Importo == 0)
            {
                Console.WriteLine("errore nella ricarica");
            }
            else
            {
                Console.WriteLine("ricarica avvenuta");
            }
                return RedirectToPage("Account");

        }
        public async Task<IActionResult> OnGet()
        {
            String token = HttpContext.Session.GetString("JWToken");
            if (token == null)
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }
            Username = await JwtUtil.ValidateAndExtractClaimAsync(token, "username");
            return Page();
        }

    }
}
