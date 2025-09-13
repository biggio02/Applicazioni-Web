using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobiShare.Models;
using MobiShare.Services;

namespace MobiShare.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IValidator<Utente> _validator;
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public Token token { get; set; }
        private readonly ITokenService _tokenService;
        public LoginModel(ITokenService tokenService, IValidator<Utente> validator)
        {
            _tokenService = tokenService;
            _validator = validator;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ValidationResult result = await _validator.ValidateAsync(new Utente(Username,Password), options => options.IncludeRuleSets("Login"));

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return Page();
            }

            token = await _tokenService.getToken(Username, Password);
            Console.WriteLine("Token ricevuto: " + token.token);
            if (token.token == null)
            {
                Console.WriteLine("token vuoto");
                TempData["Message"] = "Username e/o password errati";
                return RedirectToPage("Login");
            }
            else
            {
                HttpContext.Session.SetString("JWToken", token.token);
                Console.WriteLine("Token: " + token.token);
                return RedirectToPage("Account");
            }

        }
        public IActionResult OnGet()
        {
            string sessione = HttpContext.Session.GetString("JWToken");
            if (sessione == null)
            {
                return Page();
            }
            return RedirectToPage("Account");
            
        }
    }
}
