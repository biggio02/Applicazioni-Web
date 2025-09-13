using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobiShare.Models;
using MobiShare.Services;
using MobiShare.Utils;

namespace MobiShare.Pages
{
    public class MezziModel : PageModel
    {
        private readonly IMezzoService _mezzoService;
        public List<Mezzo> Mezzi { get; set; }
        public string Username { get; set; }
        [BindProperty(SupportsGet = true)]
        public string TipoMezzo { get; set; }
        [BindProperty(SupportsGet = true)]
        public int offset { get; set; } = 0;
        [BindProperty(SupportsGet = true)]
        public int limit { get; set; } = 10;
        [BindProperty]
        public string IdMezzo { get; set; }
        public MezziModel(IMezzoService mezzoService)
        {
            _mezzoService = mezzoService;
        }
        public async Task<IActionResult> OnGet()
        {
            string token = HttpContext.Session.GetString("JWToken");
            Username = await JwtUtil.ValidateAndExtractClaimAsync(token, "username");
            if (token == null || Username!="admin")
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }
            

            Mezzi = await _mezzoService.getMezzi(token, $"?offset={offset}&limit={limit}&tipoMezzo={TipoMezzo}");

            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            string token = HttpContext.Session.GetString("JWToken");
            if (token == null)
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }

            if(await _mezzoService.deleteMezzo(token, IdMezzo) != null)
            {
                Console.WriteLine("Mezzo eliminato");
            }
            else
            {
                Console.WriteLine("Mezzo NON eliminato");
            }
            return Redirect("/Mezzi"); ;
        }

        public async Task<IActionResult> OnPostUpdate()
        {
            string token = HttpContext.Session.GetString("JWToken");
            if (token == null)
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }

            if (await _mezzoService.patchMezzo(token, IdMezzo) != null)
            {
                Console.WriteLine("Mezzo eliminato");
            }
            else
            {
                Console.WriteLine("Mezzo NON eliminato");
            }
            return Redirect("/Mezzi"); ;
        }
    }
}
