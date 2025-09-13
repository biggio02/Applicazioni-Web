using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobiShare.Models;
using MobiShare.Services;
using MobiShare.Utils;

namespace MobiShare.Pages
{
    public class ModificaPrezziModel : PageModel
    {
        [BindProperty]
        public string tipoMezzo { get; set; }
        [BindProperty]
        public double costoFisso { get; set; }
        [BindProperty]
        public double costoVariabile { get; set; }

        public string Username { get; set; }
        private readonly ICostoService _costoService;
        public ModificaPrezziModel(ICostoService costoService)
        {
            _costoService = costoService;
        }
        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JWToken");
            Username = await JwtUtil.ValidateAndExtractClaimAsync(token, "username");
            if (token == null || Username != "admin")
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            string token = HttpContext.Session.GetString("JWToken");
            Console.WriteLine($"Paramentri:{tipoMezzo}{costoFisso},{costoVariabile} ");
            Costo c = await _costoService.putCosto(tipoMezzo, token, costoVariabile, costoFisso);
            if (c.TipoMezzo == null)
            {
                Console.WriteLine("Errore cambio prezzo");

            }
            else
            {
                Console.WriteLine("Prezzo cambiato");
            }

            return Redirect("/Index");
        }
    }
}
