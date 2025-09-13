using MobiShare.Models;

namespace MobiShare.Services
{
    public interface ICostoService
    {
        public Task<List<Costo>> getCosto();
        public Task<Costo> putCosto(string tipoMezzo, string token, double costoVariabile, double costoFisso);
    }
}
