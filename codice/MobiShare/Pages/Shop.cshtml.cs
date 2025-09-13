using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobiShare.Models;
using MobiShare.Services;
using MobiShare.Utils;

namespace MobiShare.Pages
{
    public class ShopModel : PageModel
    {
        [BindProperty]
        public int valore { get; set; }
        public string Username { get; set; }
        public CartaPrepagata prepagata { get; set; }
        private readonly ICartaPrepagataService _cartaPrepagataService;

        public ShopModel(ICartaPrepagataService cartaPrepagataService)
        {
            _cartaPrepagataService = cartaPrepagataService;
        }

        public async void OnGet()
        {
            string token = HttpContext.Session.GetString("JWToken");
            if (token == null)
            {
                Console.WriteLine("token non settato");
            }
            Username = await JwtUtil.ValidateAndExtractClaimAsync(token, "username");
            if (TempData["GiftcardJson"] is string json)
            {
                prepagata = JsonSerializer.Deserialize<CartaPrepagata>(json);
            }

        }

        public async Task<IActionResult> OnPost()
        {
            string token = HttpContext.Session.GetString("JWToken");
            if (token == null)
            {
                Console.WriteLine("token non settato");
                return Redirect("/Index");
            }

            CartaPrepagata c = await _cartaPrepagataService.postCartaPrepagata(valore, token);
            if (c.Valore == 0)
            {
                TempData["errore"] = "errore";
            }
            else
            {
                TempData["GiftcardJson"] = JsonSerializer.Serialize(c);
            }
            return RedirectToPage("Shop");

        }
    }
}
