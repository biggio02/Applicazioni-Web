using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MobiShare.Models;
using MobiShare.Services;

namespace MobiShare.Pages
{
    public class IndexModel : PageModel
    {
        public List<Costo> costi { get; set; }
        private readonly ILogger<IndexModel> _logger;
        private readonly ICostoService _costoService;

        public IndexModel(ILogger<IndexModel> logger, ICostoService costoService)
        {
            _logger = logger;
            _costoService = costoService;
        }

        public async Task OnGet()
        {
            costi = await _costoService.getCosto();
        }
    }
}
