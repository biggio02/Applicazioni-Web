using MobiShare.Models;

namespace MobiShare.Services
{
    public interface IParcheggioService
    {
        public Task<Parcheggio> postParcheggio(string indirizzo,string token);
        public Task<List<Parcheggio>> getParcheggio();
        public Task<Messaggio> deleteParcheggio(string idParcheggio, string token);
        public Task<List<Mezzo>> getAllMezziFromParcheggio(int idParcheggio);
    }
}
