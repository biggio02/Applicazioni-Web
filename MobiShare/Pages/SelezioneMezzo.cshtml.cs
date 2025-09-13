using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobiShare.Models;
using MobiShare.Services;
using MobiShare.Utils;

namespace MobiShare.Pages
{
    public class SelezioneMezzoModel : PageModel
    {
        public string Username;
        public List<Mezzo> mezzi;
        IParcheggioService _parcheggioService;
        ICorsaService _corsaService;

        [BindProperty(SupportsGet = true)]
        public string nome { get; set; }
        [BindProperty]
        public int idMezzo { get; set; }
        [BindProperty]
        public string tipoMezzo { get; set; }
        public int parcheggio { get; set; }
        public SelezioneMezzoModel(IParcheggioService parcheggioService, ICorsaService corsaService)
        {
            _parcheggioService = parcheggioService;
            _corsaService = corsaService;
        }
        public async Task<IActionResult> OnGet(int parcheggio)
        {
            string token = HttpContext.Session.GetString("JWToken");
            Username = await JwtUtil.ValidateAndExtractClaimAsync(token, "username");
            if (token == null)
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }
            mezzi=await _parcheggioService.getAllMezziFromParcheggio(parcheggio);
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            string token = HttpContext.Session.GetString("JWToken");
            int idUtente = int.Parse(await JwtUtil.ValidateAndExtractClaimAsync(token, "sub"));
            Corsa c = await _corsaService.postCorsa(idUtente,idMezzo,token);
            if (c.IdCorsa == 0)
            {
                return Redirect("Index");
            }

            return Redirect($"/Termina?idCorsa={c.IdCorsa}&nome={nome}&tipoMezzo={tipoMezzo}");
        }
    }
}
