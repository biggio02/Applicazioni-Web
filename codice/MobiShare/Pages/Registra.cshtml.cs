using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobiShare.Models;
using MobiShare.Services;
using FluentValidation.Results;

namespace MobiShare.Pages
{
    public class RegistraModel : PageModel
    {
        private readonly IValidator<Utente> _validator;
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Email{ get; set; }
        [BindProperty]
        public string Password { get; set; }
        public Utente utente { get; set; }

        private readonly IAccountService _accountService;
        public RegistraModel(IAccountService accountService, IValidator<Utente> validator)
        {
            _accountService = accountService;
            _validator = validator;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ValidationResult result = await _validator.ValidateAsync(new Utente(Username, Email, Password), options => options.IncludeRuleSets("Registration"));

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return Page();
            }

            utente =await _accountService.postAccount(Username, Email,Password);
            if (utente.Email != null)
            {
                TempData["Message"]= "success";
            }
            else
            {
                TempData["Message"] = "error";
            }
            return RedirectToPage("Registra");
        }
    }
}
