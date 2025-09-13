using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobiShare.Models;
using MobiShare.Services;
using MobiShare.Utils;

namespace MobiShare.Pages
{
    public class SelezioneParcheggioModel : PageModel
    {
        string Username;
        public List<Parcheggio> parcheggi { get; set; }
        private readonly IParcheggioService _parcheggioService;

        public SelezioneParcheggioModel(IParcheggioService parcheggioService)
        {
            _parcheggioService = parcheggioService;
        }
        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JWToken");
            Username = await JwtUtil.ValidateAndExtractClaimAsync(token, "username");
            if (token == null)
            {
                Console.WriteLine("token non settato");
                return Redirect("/Login");
            }
            parcheggi = await _parcheggioService.getParcheggio();
            return Page();
        }
    }
}
