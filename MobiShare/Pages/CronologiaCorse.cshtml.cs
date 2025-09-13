using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobiShare.Models;
using MobiShare.Services;
using MobiShare.Utils;

namespace MobiShare.Pages
{
    public class CronologiaCorseModel : PageModel
    {
        private readonly IAccountService _accountService;
        public List<CronologiaCorse> corse { get; set; }
        public string Username { get; set; }
        [BindProperty(SupportsGet = true)]
        public int offset { get; set; } = 0;
        [BindProperty(SupportsGet = true)]
        public int limit { get; set; } = 10;
        public CronologiaCorseModel(IAccountService accountService)
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
            Console.WriteLine("username da account: " + Username);
            corse =await _accountService.getCorse(Username, $"?offset={offset}&limit={limit}", token);
            return Page();
        }
    }
}
