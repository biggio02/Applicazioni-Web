using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobiShare.Models;
using MobiShare.Services;
using MobiShare.Utils;

namespace MobiShare.Pages
{
    public class AggiungiMezzoModel : PageModel
    {
        [BindProperty]
        public string tipo_mezzo { get; set; }
        [BindProperty]
        public int id_parcheggio { get; set; }
        [BindProperty]
        public int batteria { get; set; }
        [BindProperty]
        public int prelevabile { get; set; }

        public string Username { get; set; }
        public List<Parcheggio> parcheggi { get; set; }
        private readonly IParcheggioService _parcheggioService;
        private readonly IMezzoService _mezzoService;

        public AggiungiMezzoModel(IParcheggioService parcheggioService, IMezzoService mezzoService)
        {
            _parcheggioService = parcheggioService;
            _mezzoService = mezzoService;
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
            parcheggi=await _parcheggioService.getParcheggio();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            string token = HttpContext.Session.GetString("JWToken");
            Username = await JwtUtil.ValidateAndExtractClaimAsync(token, "username");
            if (token == null || Username != "admin")
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }
            Mezzo m = await _mezzoService.postMezzo(token,tipo_mezzo,id_parcheggio,prelevabile,batteria);
            if (m.TipoMezzo == null)
            {
                Console.WriteLine("Errore mezzo");
            }
            else
            {
                Console.WriteLine("Mezzo Aggiunto");
            }
            return Redirect("/Account");
        }
    }
}
