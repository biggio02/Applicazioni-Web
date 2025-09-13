using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobiShare.Models;
using MobiShare.Services;
using MobiShare.Utils;

namespace MobiShare.Pages
{
    public class AggiungiParcheggioModel : PageModel
    {
        [BindProperty]
        public string Indirizzo { get; set; }

        [BindProperty]
        public string IdParcheggio { get; set; }

        public string Username { get; set; }

        public List<Parcheggio> parcheggi { get; set; }
        private readonly IParcheggioService _parcheggioService;
        public AggiungiParcheggioModel(IParcheggioService parcheggioService)
        {
            _parcheggioService = parcheggioService;
        }
        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JWToken");
            Username = await JwtUtil.ValidateAndExtractClaimAsync(token, "username");
            if (token == null || Username !="admin")
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }
            parcheggi= await _parcheggioService.getParcheggio();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            string token = HttpContext.Session.GetString("JWToken");
            Parcheggio p = await _parcheggioService.postParcheggio(Indirizzo,token);
            if (p == null)
            {
                Console.WriteLine("Errore parcheggio");
                return Redirect("/Index");

            }
            else
            {
                Console.WriteLine("Parcheggio Aggiunto");
            }

            return Redirect("/AggiungiParcheggio");
        }

        public async Task<IActionResult> OnPostDelete()
        {
            string token = HttpContext.Session.GetString("JWToken");
            Console.WriteLine($"Delete premuta con id {IdParcheggio}");
            Messaggio m = await _parcheggioService.deleteParcheggio(IdParcheggio,token);
            Console.WriteLine(m.message);
            return Redirect("/AggiungiParcheggio");
        }
    }
}
