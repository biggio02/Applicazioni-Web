using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobiShare.Models;
using MobiShare.Services;
using MobiShare.Utils;

namespace MobiShare.Pages
{
    public class TerminaModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string nome { get; set; }

        [BindProperty]
        public int id_parcheggio { get; set; }


        [BindProperty(SupportsGet = true)]
        public int idCorsa { get; set; }
        [BindProperty(SupportsGet = true)]
        public string tipoMezzo { get; set; }
        [BindProperty]
        public string feedback { get; set; }
        private readonly ICorsaService _corsaService;
        private readonly IParcheggioService _parcheggioService;
        public List<Parcheggio> parcheggi { get; set; }

        public TerminaModel(ICorsaService corsaService, IParcheggioService parcheggioService)
        {
            _corsaService = corsaService;
            _parcheggioService = parcheggioService;
        }
        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JWToken");
            if (token == null)
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }
            parcheggi = await _parcheggioService.getParcheggio();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            string token = HttpContext.Session.GetString("JWToken");
            if (token == null)
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }
            if (feedback == null)
            {
                feedback = "";
            }
            Corsa c = await _corsaService.putCorsa(idCorsa, feedback, id_parcheggio, token);
            if (c?.IdCorsa == null)
            {
                return Redirect("/Index");
            }
            return Redirect("/CronologiaCorse");

        }
    }
}
