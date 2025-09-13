using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobiShare.Models;
using MobiShare.Services;
using MobiShare.Utils;

namespace MobiShare.Pages
{
    public class CronologiaPagamentiModel : PageModel
    {
        private readonly IAccountService _accountService;
        public List<Ricarica> Ricariche { get; set; }
        public string Username { get; set; }
        [BindProperty(SupportsGet = true)]
        public int offset { get; set; } = 0;
        [BindProperty(SupportsGet = true)]
        public int limit { get; set; } = 10;
        public CronologiaPagamentiModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JWToken");
            if (token == null)
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }
            Username = await JwtUtil.ValidateAndExtractClaimAsync(token, "username");
            Ricariche = await _accountService.getRicariche(Username,$"?offset={offset}&limit={limit}", token);
            return Page();
        }
    }
}
