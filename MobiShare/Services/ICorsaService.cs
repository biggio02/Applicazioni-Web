using MobiShare.Models;

namespace MobiShare.Services
{
    public interface ICorsaService
    {
        public Task<Corsa>postCorsa(int utente,int mezzo, string token);
        public Task<Corsa> putCorsa(int idCorsa, string feedback, int parcheggio, string token);
    }
}
